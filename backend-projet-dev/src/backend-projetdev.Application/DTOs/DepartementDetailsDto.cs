namespace backend_projetdev.Application.DTOs
{
    public class DepartementDetailsDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<EquipesOfDepartementDto> Equipes { get; set; }
    }
}
