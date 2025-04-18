using Microsoft.EntityFrameworkCore;
using Orçan.Api.Data;
using Orçan.Core.Common;
using Orçan.Core.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Transactions;
using Orçan.Core.Responses;

namespace Orçan.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<PagedResponse<List<Transaction>?>> GetTransactionsByPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();

            var query = context.Transactions
                .AsNoTracking()
                .Where(x =>
                    x.CreatedAt >= request.StartDate &&
                    x.CreatedAt <= request.EndDate &&
                    x.UserId == request.UserId)
                .OrderBy(x => x.CreatedAt);

            var transactions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Transaction>?>(
                transactions,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar transações: {ex.Message}");
            return new PagedResponse<List<Transaction>?>(
                null,
                500,
                "Não foi possível consultar as transações.");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");

            return new Response<Transaction?>(transaction, 200, "Transação Atualizada com sucesso ");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Erro ao criar transação");
        }
    }

    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
      try
        {
            var transcation = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaiOrReceivedAt = request.PaiOrReceivedAt,
                Title = request.Title,
                Type = request.Type
            };

            await context.Transactions.AddAsync(transcation);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transcation, 201, "Transação criada com sucesso");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Erro ao criar transação");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");

            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            transaction.PaiOrReceivedAt = request.PaiOrReceivedAt;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação Atualizada com sucesso ");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Erro ao criar transação");
        }
    }
    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação Atualizada com sucesso ");
        }
        catch
        {
            return new Response<Transaction?>(null, 500, "Erro ao criar transação");
        }
    }
}
