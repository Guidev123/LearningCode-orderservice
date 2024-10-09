namespace OrderService.API.Application.Responses
{
    public class Response
    {
        public Response(bool isSuccess = true, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public static Response Success() => new();
        public static Response Error(string message) => new(false, message);

    }
    public class Response<T> : Response
    {
        public Response(T? data, bool isSucces = true, string message = "") : base(isSucces, message)
        {
            Data = data;
        }
        public T? Data { get; private set; }
        public static Response<T> Success(T data) => new(data);
        public static new Response<T> Error(string message) => new(default, false, message);
    }
}
