namespace backend_projetdev.Domain.Entities
{
    public class Paie
    {
        public int Id { get; set; }
        public string PersonneId { get; set; }
        public Personne Personne { get; set; }

        public DateTime DatePaie { get; set; }
        public decimal Montant { get; set; }
        public string Description { get; set; }
        public string Avantages { get; set; }
        public string Retenues { get; set; }
    }

}
