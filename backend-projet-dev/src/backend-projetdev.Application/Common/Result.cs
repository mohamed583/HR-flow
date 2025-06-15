
namespace backend_projetdev.Application.Common
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public static Result SuccessResult(string message = null) => new Result { Success = true, Message = message };
        public static Result Failure(string message) => new Result { Success = false, Message = message };
    }
    public class Result<T> : Result
    {
        public T Data { get; set; }

        public static Result<T> SuccessResult(T data, string message = null)
        {
            return new Result<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public new static Result<T> Failure(string message)
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}
