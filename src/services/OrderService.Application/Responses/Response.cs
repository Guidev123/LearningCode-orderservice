using System.Text.Json.Serialization;

namespace OrderService.Application.Responses
{
    public class Response<TData>
    {
        private readonly int _code;
        private const int DEFAULT_STATUS_CODE = 200;

        [JsonConstructor]
        public Response()
            => _code = DEFAULT_STATUS_CODE;

        public Response(
            TData? data,
            int code = DEFAULT_STATUS_CODE,
            string? message = null)
        {
            Data = data;
            Message = message;
            _code = code;
        }
        public Response(
        List<TData>? listData,
        int code = DEFAULT_STATUS_CODE,
        string? message = null)
        {
            ListData = listData;
            Message = message;
            _code = code;
        }
        public TData? Data { get; set; }
        public List<TData>? ListData { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess
            => _code is >= DEFAULT_STATUS_CODE and <= 299;
    }
}
