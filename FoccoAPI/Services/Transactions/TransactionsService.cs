using FoccoAPI.Database;
using FoccoAPI.Dtos;
using FoccoAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace FoccoAPI.Services.NovaPasta
{
    public class TransactionsService : ITransactionsInterface
    {
        private readonly ApplicationDbContext _context;

        public TransactionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<CreateTransactionDto>> createTransaction(CreateTransactionDto transactionDto, UserModel user)
        {
            ResponseModel<CreateTransactionDto> response = new ResponseModel<CreateTransactionDto>();

            try
            {

                var transaction = new TransactionsModel
                {
                   Name = transactionDto.Name,
                   Description = transactionDto.Description,
                   CashFlow = transactionDto.CashFlow,
                   Value = transactionDto.Value,    
                   UserId = user.Id
                };

                _context.Add(transaction);
                await _context.SaveChangesAsync();

                response.Data = transactionDto;
                response.Message = "Criado com sucesso";

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ex.Message;  
                response.Status = false;
            }

            return response;
        }


        public UserModel GetUserFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null) return null;

            int userId = int.Parse(userIdClaim.Value);

            // Simular a recuperação de um usuário baseado no ID
            var user = new UserModel
            {
                Id = userId,
                UserName = jwtToken.Claims.FirstOrDefault(c => c.Type == "User")?.Value,
                Email = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value,
                Cargo = jwtToken.Claims.FirstOrDefault(c => c.Type == "Cargo")?.Value
            };

            return user;
        }



    }
}
