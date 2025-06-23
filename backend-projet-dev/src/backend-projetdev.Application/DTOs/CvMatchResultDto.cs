using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class CvMatchResultDto
    {
        public string CandidatureId { get; set; }
        public double MatchingScore { get; set; }
    }
}
