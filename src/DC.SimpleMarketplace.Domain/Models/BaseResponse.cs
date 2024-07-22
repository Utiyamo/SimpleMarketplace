using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Models
{
    public class BaseResponse
    {
        public int Status { get; set; }
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; }

        public static BaseResponse<T> Success(T data) => new BaseResponse<T> { isSuccess = true, Data = data, Status = 200, Message = String.Empty };
        public static BaseResponse<T> Error (int status, string message) => new BaseResponse<T> { isSuccess = false, Status =  status, Message = message };
        public static BaseResponse<T> NotFound(string message) => new BaseResponse<T> { isSuccess = false, Status = 404, Message = message, };
    }
}
