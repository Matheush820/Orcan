using Orcan.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Orcan.Core.Requests.Transactions;
public class CreateTransactionRequest : Request
{
    [Required(ErrorMessage = "Titulo invalido")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo invalido")]
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    [Required(ErrorMessage = "Valo invalido")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Categoria invalido")]
    public long CategoryId { get; set; }

    [Required(ErrorMessage = "Data invalido")]
    public DateTime? PaiOrReceivedA { get; set; }
}
