using System.Text.Json.Serialization;

namespace Orçan.Core.Responses
{
    public class Response<TData>
    {
        private readonly int _code;

        // Construtor sem parâmetros
        public Response()
            => _code = Configuration.DefaultStatuscode;

        // Construtor com parâmetros (para passar dados)
        public Response(
            TData? data,
            int code = Configuration.DefaultStatuscode,
            string? message = null)
        {
            Data = data;
            Message = message;
            _code = code;
        }

        public TData? Data { get; set; }
        public string? Message { get; set; }

        [JsonIgnore] // Não serializa o campo IsSuccess
        public bool IsSuccess => _code is >= 200 and <= 299;
    }
}
