using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcan.Core;
public static class Configuration
{
    public const int DefaultPageSize = 25;
    public const int DefaultPageNumber = 1;
    public const int DefaultStatusCode = 200;


    public static string ConnectionString { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = "http://localhost:5194";  // Ajuste conforme necessário
    public static string FrontendUrl { get; set; } = "http://localhost:5105";  // Ajuste conforme necessário


}
