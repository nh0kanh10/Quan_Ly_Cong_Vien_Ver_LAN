using System;

namespace ET
{
    public class ResponseResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static ResponseResult Success()
        {
            return new ResponseResult { IsSuccess = true };
        }

        public static ResponseResult Error(string message)
        {
            return new ResponseResult { IsSuccess = false, ErrorMessage = message };
        }
    }

    public class ResponseResult<T> : ResponseResult
    {
        public T Data { get; set; }

        public static ResponseResult<T> Success(T data)
        {
            return new ResponseResult<T> { IsSuccess = true, Data = data };
        }

        public new static ResponseResult<T> Error(string message)
        {
            return new ResponseResult<T> { IsSuccess = false, ErrorMessage = message };
        }
    }
}
