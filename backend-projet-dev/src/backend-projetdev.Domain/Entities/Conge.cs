using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Domain.Entities
{
    public class Conge
    {
        public int Id { get; set; }
        public string EmployeId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Type { get; set; }
        public Status StatusConge { get; set; }

        public Employe Employe { get; set; }
    }
}
