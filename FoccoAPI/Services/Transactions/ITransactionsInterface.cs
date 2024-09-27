using FoccoAPI.Dtos;
using FoccoAPI.Models;

namespace FoccoAPI.Services.NovaPasta
{
    public interface ITransactionsInterface
    {
        Task<ResponseModel<CreateTransactionDto>> createTransaction(CreateTransactionDto transactionDto, UserModel user);
        Task<ResponseModel<UpdateTransactionDto>> UpdateTransaction(UpdateTransactionDto transactionDto, UserModel user);
        Task<ResponseModel<List<TransactionsModel>>> GetAllTransactions(UserModel user);
        Task<ResponseModel<TransactionsModel>> GetTransactionsById(int id, UserModel user);
        Task<ResponseModel<TransactionsModel>> DeleteTransaction(int id, UserModel user);
    }
}
