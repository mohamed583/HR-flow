namespace backend_projetdev.Domain.Entities
{
    public class Candidat : Personne
    {
        public string Description { get; set; }
        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();
    }
}
