using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.Wrapper
{
    public class ServiceResponse<T> : BaseResponse
    {
        //public Guid Id { get; set; }
        //public string? Message { get; set; }
        //public int Status { get; set; }
        //public T? Data { get; set; }

        //public static BaseResponse<T> Result(T Data, string Message, int Status) => new BaseResponse<T> { Data = Data, Message = Message, Status = Status };

        public T? Value { get; set; }

        public ServiceResponse(T value)
        {
            Value = value;
        }

    }
}
