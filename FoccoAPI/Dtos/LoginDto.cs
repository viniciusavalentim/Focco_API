using System.ComponentModel.DataAnnotations;

namespace FoccoAPI.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O campo Email é obrigatório"), EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string? Password { get; set; }
    }
}
