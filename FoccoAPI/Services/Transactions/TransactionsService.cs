﻿using FoccoAPI.Database;
using FoccoAPI.Dtos;
using FoccoAPI.Enum;
using FoccoAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ResponseModel<UpdateTransactionDto>> UpdateTransaction(UpdateTransactionDto transactionDto, UserModel user)
        {
            ResponseModel<UpdateTransactionDto> response = new ResponseModel<UpdateTransactionDto>();

            try
            {
                var existingTransaction = await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(t => t.Id == transactionDto.Id && t.UserId == user.Id);

                if (existingTransaction != null)
                {

                    existingTransaction.Name = transactionDto.Name;
                    existingTransaction.Description = transactionDto.Description;
                    existingTransaction.Value = transactionDto.Value;
                    existingTransaction.CashFlow = transactionDto.CashFlow;

                    _context.Transactions.Update(existingTransaction);
                    await _context.SaveChangesAsync();

                    response.Data = transactionDto;
                    response.Message = "Editado com sucesso";
                }
                else
                {
                    response.Data = null;
                    response.Message = "Trnsação não encontrada.";
                    response.Status = false;

                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ex.Message;
                response.Status = false;
            }

            return response;
        }

        public async Task<ResponseModel<List<TransactionsModel>>> GetAllTransactions(DateTime currentDate,UserModel user)
        {
            ResponseModel<List<TransactionsModel>> response = new ResponseModel<List<TransactionsModel>>();

            try
            {
                var AllTransactions = await _context.Transactions.Where(t => t.UserId == user.Id && t.CreatedAt.Month == currentDate.Month && t.CreatedAt.Year == currentDate.Year).ToListAsync();

                if (AllTransactions == null)
                {
                    response.Data = [];
                    response.Message = "Nenhum dado foi encontrado";
                }
                else
                {
                    response.Data = AllTransactions;
                    response.Message = "Dados Encontrados com sucesso";
                }
            }
            catch (Exception e)
            {
                response.Data = null;
                response.Message = e.Message;
                response.Status = false;
            }

            return response;
        }

        public async Task<ResponseModel<TransactionsModel>> GetTransactionsById(int id, UserModel user)
        {
            ResponseModel<TransactionsModel> response = new ResponseModel<TransactionsModel>();

            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);


                if (transaction == null)
                {
                    response.Data = null;
                    response.Message = "Transacão não encontrada";
                }
                else
                {
                    response.Data = transaction;
                    response.Message = "Dados Encontrados com sucesso";
                }
            }
            catch (Exception e)
            {
                response.Data = null;
                response.Message = e.Message;
                response.Status = false;
            }


            return response;
        }

        public async Task<ResponseModel<TransactionsModel>> DeleteTransaction(int id, UserModel user)
        {
            ResponseModel<TransactionsModel> response = new ResponseModel<TransactionsModel>();

            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

                if (transaction == null)
                {
                    response.Data = null;
                    response.Message = "Transacão não encontrada";
                }
                else
                {

                    transaction.Status = 'X';

                    await _context.SaveChangesAsync();

                    response.Data = transaction;
                    response.Message = "Dados Encontrados com sucesso";
                }
            }
            catch (Exception e)
            {
                response.Data = null;
                response.Message = e.Message;
                response.Status = false;
            }


            return response;
        }

        public async Task<ResponseModel<double>> GetCurrentBalance(DateTime currentDate,UserModel user)
        {
            ResponseModel<double> response = new ResponseModel<double>();

            try
            {
                var AllTransactions = await _context.Transactions.Where(t => t.UserId == user.Id && t.CreatedAt.Month == currentDate.Month && t.CreatedAt.Year == currentDate.Year).ToListAsync();


                double currentBalance = 0;


                foreach (var transaction in AllTransactions)
                {
                    if (transaction.CashFlow == Enum.CashFlowEnum.Income)
                    {
                        currentBalance += transaction.Value;
                    }
                    else if (transaction.CashFlow == Enum.CashFlowEnum.Expense)
                    {
                        currentBalance -= transaction.Value;
                    }
                    else if (transaction.CashFlow == Enum.CashFlowEnum.Investment)
                    {
                        currentBalance -= transaction.Value;
                    }
                }

                if (AllTransactions == null)
                {
                    response.Data = 0;
                    response.Message = "Sem transações";
                    response.Status = true;
                    return response;
                }


                response.Data = currentBalance;
                response.Message = "Current Balance Found";
                return response;

            }
            catch (Exception e)
            {
                response.Data = 0;
                response.Message = e.Message;
                response.Status = false;
            }


            return response;
        }

        public async Task<ResponseModel<double>> GetCashFlowById(int id, DateTime currentDate,UserModel user)
        {
            ResponseModel<double> response = new ResponseModel<double>();

            try
            {
                CashFlowEnum cashFlow = (CashFlowEnum)id;

                var currentMonth = DateTime.Now.Month;
                var currentYear = DateTime.Now.Year;

                var AllTransactions = await _context.Transactions
                                            .Where(t => t.UserId == user.Id && 
                                                        t.CashFlow == cashFlow && 
                                                        t.CreatedAt.Month == currentDate.Month && 
                                                        t.CreatedAt.Year == currentDate.Year).ToListAsync();

                double totalCashFlow = AllTransactions.Sum(t => t.Value);

                if (AllTransactions == null)
                {
                    response.Data = 0;
                    response.Message = "Sem transações";
                    response.Status = true;
                    return response;
                }

                response.Data = totalCashFlow;
                response.Message = "Current Balance Found";
                return response;

            }
            catch (Exception e)
            {
                response.Data = 0;
                response.Message = e.Message;
                response.Status = false;
            }

            return response;
        }
    }
}
