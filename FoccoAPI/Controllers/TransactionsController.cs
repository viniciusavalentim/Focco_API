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


        [HttpPost("create")]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionDto create)
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

                return Ok(await _transactionsInterface.createTransaction(create, user));
            }

            return Unauthorized();

        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateTransaction([FromBody] UpdateTransactionDto update)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("Id")?.Value;

                var user = new UserModel
                {
                    Id = int.Parse(userId),
                };

                var response = await _transactionsInterface.UpdateTransaction(update, user);

                if (response.Status == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);

            }

            return Unauthorized();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTransactionsById(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("Id")?.Value;

                var user = new UserModel
                {
                    Id = int.Parse(userId),
                };

                var response = await _transactionsInterface.GetTransactionsById(id, user);

                if (response.Status == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);

            }

            return Unauthorized();

        }

        [HttpPut("delete/{id}")]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("Id")?.Value;

                var user = new UserModel
                {
                    Id = int.Parse(userId),
                };

                var response = await _transactionsInterface.DeleteTransaction(id, user);

                if (response.Status == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);

            }

            return Unauthorized();

        }

        [HttpGet("All")]
        public async Task<ActionResult> GetAllTransactions()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("Id")?.Value;

                var user = new UserModel
                {
                    Id = int.Parse(userId),
                };

                var response = await _transactionsInterface.GetAllTransactions(user);

                if (response.Status == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);

            }

            return Unauthorized();

        }

    }
}
