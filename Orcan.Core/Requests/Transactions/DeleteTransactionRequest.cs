﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcan.Core.Requests.Transactions;
public class DeleteTransactionRequest : Request
{
    public long Id { get; set; }
}
