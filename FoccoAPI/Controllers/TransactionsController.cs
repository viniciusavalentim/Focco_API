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

        [HttpGet("all/{currentDate}")]
        public async Task<ActionResult> GetAllTransactions(DateTime currentDate)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("Id")?.Value;

                var user = new UserModel
                {
                    Id = int.Parse(userId),
                };

                var response = await _transactionsInterface.GetAllTransactions(currentDate, user);

                if (response.Status == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);

            }

            return Unauthorized();

        }

        [HttpGet("balance/{currentDate}")]
        public async Task<ActionResult> GetCurrentBalance(DateTime currentDate)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("Id")?.Value;

                var user = new UserModel
                {
                    Id = int.Parse(userId),
                };

                var response = await _transactionsInterface.GetCurrentBalance(currentDate, user);

                if (response.Status == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);

            }

            return Unauthorized();

        }

        [HttpGet("cashflow/{id}/{currentDate}")]
        public async Task<ActionResult> GetCashFlow(int id, DateTime currentDate)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("Id")?.Value;

                var user = new UserModel
                {
                    Id = int.Parse(userId),
                };

                var response = await _transactionsInterface.GetCashFlowById(id,currentDate, user);

                if (response.Status == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);

            }

            return Unauthorized();

        }

        [HttpGet("user")]
        public async Task<ActionResult> GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("User")?.Value;

                if (userId == null)
                {
                    return BadRequest(userId);
                }

                return Ok(userId);

            }

            return Unauthorized();

        }

    }
}
