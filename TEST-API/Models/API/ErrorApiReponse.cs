namespace TEST_API.Models.API
{
    public class ErrorApiReponse
    {
        public string? Description { get; set; }
        public string? Message { get; set; }
    }

    public class ServiceResults<T>
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public ServiceResults(bool success, int code, string message, T? data)
        {
            Message = message;
            Data = data;
            Success = success;
            Code = code;
        }
    }
}
