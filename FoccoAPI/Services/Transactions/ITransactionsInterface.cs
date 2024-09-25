using FoccoAPI.Dtos;
using FoccoAPI.Models;

namespace FoccoAPI.Services.NovaPasta
{
    public interface ITransactionsInterface
    {
        Task<ResponseModel<CreateTransactionDto>> createTransaction(CreateTransactionDto transactionDto, UserModel user);
    }
}
