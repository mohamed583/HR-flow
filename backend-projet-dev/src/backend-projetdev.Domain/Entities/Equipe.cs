namespace backend_projetdev.Domain.Entities
{
    public class Equipe
    {
        public int Id { get; set; }
        public int DepartementId { get; set; }
        public Departement Departement { get; set; }

        public string Nom { get; set; }
        public ICollection<Employe> Employes { get; set; } = new List<Employe>();
    }

}
