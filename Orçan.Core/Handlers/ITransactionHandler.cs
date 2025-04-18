

using Orçan.Core.Models;
using Orçan.Core.Requests.Transactions;
using Orçan.Core.Responses;

namespace Orçan.Core.Handlers;
public interface ITransactionHandler
{
    Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
    Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
    Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
    Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
    Task<PagedResponse<List<Transaction>?>> GetTransactionsByPeriodAsync(GetTransactionsByPeriodRequest request);


}
