namespace backend_projetdev.Domain.Entities
{
    public class Formateur : Personne
    {
        public decimal Salaire { get; set; }
        public string Domaine { get; set; }
        public string Description { get; set; }

        public ICollection<Formation> Formations { get; set; } = new List<Formation>();
    }
}
