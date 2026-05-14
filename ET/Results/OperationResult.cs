namespace ET.Results
{

    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string ErrorMessage => Message;

        #region Factory methods — tạo nhanh kết quả

        public static OperationResult Ok(string msg = "Thành công")
            => new OperationResult { Success = true, Message = msg };

        /// <summary>
        /// Tạo kết quả thành công kèm dữ liệu trả về.
        /// </summary>
        /// <param name="data">Dữ liệu đính kèm (entity, list, ID vừa insert...)</param>
        /// <param name="msg">Thông báo hiển thị cho người dùng</param>
        /// <returns>OperationResult với Success = true</returns>
        public static OperationResult Ok(object data, string msg = "Thành công")
            => new OperationResult { Success = true, Message = msg, Data = data };

        /// <summary>
        /// Tạo kết quả thất bại.
        /// </summary>
        /// <param name="msg">Lý do thất bại, hiển thị cho người dùng</param>
        /// <returns>OperationResult với Success = false</returns>
        public static OperationResult Fail(string msg)
            => new OperationResult { Success = false, Message = msg };

        #endregion
    }
    public class OperationResult<T> : OperationResult
    {
        public new T Data
        {
            get => base.Data == null ? default(T) : (T)base.Data;
            set => base.Data = value;
        }

        public static OperationResult<T> Ok(T data, string msg = "Thành công")
            => new OperationResult<T> { Success = true, Message = msg, Data = data };

        public new static OperationResult<T> Fail(string msg)
            => new OperationResult<T> { Success = false, Message = msg };

        public new string ErrorMessage => Message;
    }
}
