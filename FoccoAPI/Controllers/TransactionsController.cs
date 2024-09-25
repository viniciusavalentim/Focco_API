using Azure;
using FoccoAPI.Dtos;
using FoccoAPI.Models;
using FoccoAPI.Services.NovaPasta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoccoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        private readonly ITransactionsInterface _transactionsInterface;

        public TransactionsController(ITransactionsInterface transactionsService)
        {
            _transactionsInterface = transactionsService;
        }


        [HttpPost("Create")]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionDto transactionDto)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("Id")?.Value;
                var userEmail = User.FindFirst("Email")?.Value;

                var user = new UserModel
                {
                    Id = int.Parse(userId),
                    Email = userEmail,
                };

                return Ok(await _transactionsInterface.createTransaction(transactionDto, user));
            }

            return Unauthorized();

        }

    }
}
