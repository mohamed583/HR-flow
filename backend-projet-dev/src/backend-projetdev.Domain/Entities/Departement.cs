namespace backend_projetdev.Domain.Entities
{
    public class Departement
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Equipe> Equipes { get; set; } = new List<Equipe>();
    }
}
