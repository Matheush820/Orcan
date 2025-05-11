using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcan.Core.Requests.Categories;
public class GetCategoryByIdRequest : Request
{
    public long Id { get; set; }
}
