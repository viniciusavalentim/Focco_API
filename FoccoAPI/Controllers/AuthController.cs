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

                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<object>();
                    
                    return Ok(authResponse);
                }

                return Unauthorized("Falha na autenticação.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
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
