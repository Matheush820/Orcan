using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Orçan.Core.Common;
public static class DateTimeExtension
{
    public static DateTime GetFirstDay(this DateTime date, int? year = null, int? month = null)
    {
        return new DateTime(
            year ?? date.Year,
            month ?? date.Month,
            1
        );
    }

    public static DateTime GetLastDay(this DateTime date, int? year = null, int? month = null)
    {
        return new DateTime(
           year ?? date.Year,
           month ?? date.Month,
           1
       ).AddMonths(1).AddDays(-1);
    }
}
