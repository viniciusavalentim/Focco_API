using System.ComponentModel.DataAnnotations;

namespace FoccoAPI.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "O campo User é obgratório")]
        public string? User { get; set; }

        [Required(ErrorMessage = "O campo Email é obgratório"), EmailAddress(ErrorMessage = "Email inválido, tente novamente")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obgratório")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Senhas nao coincidem")]
        public string? ConfirmPassword { get; set; }

    }
}
