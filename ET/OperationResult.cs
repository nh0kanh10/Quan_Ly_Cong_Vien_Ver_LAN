using System;

namespace ET
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Exception InternalException { get; set; }

        public static OperationResult Success()
        {
            return new OperationResult { IsSuccess = true };
        }

        public static OperationResult Success(string message)
        {
            return new OperationResult { IsSuccess = true, ErrorMessage = message };
        }

        public static OperationResult Failed(string message, Exception ex = null)
        {
            return new OperationResult { IsSuccess = false, ErrorMessage = message, InternalException = ex };
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T> { IsSuccess = true, Data = data };
        }

        public new static OperationResult<T> Failed(string message, Exception ex = null)
        {
            return new OperationResult<T> { IsSuccess = false, ErrorMessage = message, InternalException = ex };
        }
    }
}
