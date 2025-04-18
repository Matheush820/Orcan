﻿using Orçan.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orçan.Core.Requests.Transactions;
public class UpdateTransactionRequest : Request
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Titulo invalido")]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "Tipo invalido")]
    public ETransactionType Type { get; set; }
    [Required(ErrorMessage = "Valor invalido")]
    public decimal Amount { get; set; }
    [Required(ErrorMessage = "Data invalida")]
    public long CategoryId { get; set; }
    [Required(ErrorMessage = "Data invalida")]
    public DateTime? PaiOrReceivedAt { get; set; }
}
