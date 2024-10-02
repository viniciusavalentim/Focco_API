using System.ComponentModel.DataAnnotations;

namespace FoccoAPI.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "O campo User é obrigatório")]
        public string? User { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório"), EmailAddress(ErrorMessage = "Email inválido, tente novamente")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Senhas nao coincidem")]
        public string? ConfirmPassword { get; set; }

    }
}
