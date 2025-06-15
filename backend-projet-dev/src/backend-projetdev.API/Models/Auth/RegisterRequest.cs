namespace backend_projetdev.API.Models.Auth
{
    
    public class RegisterRequest
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
