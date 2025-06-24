using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Infrastructure.Persistence;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace backend_projetdev.Infrastructure.Services
{
    public class CvMatchingService : ICvMatchingService
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IWebHostEnvironment _env;

        public CvMatchingService(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration config, IWebHostEnvironment env)
        {
            _context = context;
            _httpClient = clientFactory.CreateClient();
            _apiKey = config["OpenRouterApiKey"];
            _env = env;
        }

        public async Task<List<CvMatchResultDto>> GetMatchingsAsync(int posteId)
        {
            var poste = await _context.Postes.FirstOrDefaultAsync(p => p.Id == posteId);
            if (poste == null) return new List<CvMatchResultDto>();

            var candidatures = await _context.Candidatures
                .Where(c => c.PosteId == posteId)
                .ToListAsync();

            var results = new List<CvMatchResultDto>();

            foreach (var candidature in candidatures)
            {
                var cvText = ExtractTextFromPdf(Path.Combine(_env.WebRootPath, candidature.CVPath));
                var score = await GetMatchingScoreAsync(cvText, poste.Description);

                results.Add(new CvMatchResultDto
                {
                    CandidatureId = candidature.Id,
                    MatchingScore = score
                });
            }

            return results;
        }

        private string ExtractTextFromPdf(string path)
        {
            if (!File.Exists(path)) return "";

            using var pdfReader = new PdfReader(path);
            using var pdfDoc = new PdfDocument(pdfReader);
            var strategy = new SimpleTextExtractionStrategy();

            var sb = new StringBuilder();
            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                var text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), strategy);
                sb.AppendLine(text);
            }

            return sb.ToString();
        }

        private async Task<double> GetMatchingScoreAsync(string cvText, string jobDescription)
        {
            var prompt = $@"Tu es un assistant qui évalue la correspondance entre un CV et une description de poste.
Donne-moi un score en pourcentage (0 à 100) représentant à quel point ce CV correspond à la description.
CV : {cvText}
Description du poste : {jobDescription}
Réponds uniquement par un nombre.";

            var body = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "system", content = "Tu es un assistant de matching de CV." },
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            Console.WriteLine(doc.RootElement.ToString());
            var result = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return double.TryParse(result.Replace("%", "").Trim(), out var score) ? score : 0;
        }
    }
}
