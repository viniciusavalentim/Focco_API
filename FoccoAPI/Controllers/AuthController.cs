using FoccoAPI.Dtos;
using FoccoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoccoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto login)
        {
            if (login == null)
            {
                return BadRequest("Dados Inválidos");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7020/api/Auth/login", login);

                var authResponse = await response.Content.ReadFromJsonAsync<ResponseModel<string>>();

                if (authResponse != null && authResponse.Status == false)
                {
                    return BadRequest(authResponse);

                }
                return Ok(authResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro interno no servidor", detalhe = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto register)
        {
            if (register == null)
            {
                return BadRequest("Dados Inválidos");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7020/api/Auth/register", register);

                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<object>();
                    return Ok(authResponse);
                }
                else
                {
                    // Logar a mensagem de erro retornada pela API de autenticação
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Unauthorized($"Falha na autenticação. Mensagem de erro: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
