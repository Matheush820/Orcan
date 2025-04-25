using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcan.Core.Requests.Transactions;
public class GetTransactionsByPeriodRequest : PageDRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
