namespace Onion.Cqrs.Application.Wrapper
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T? Value { get; set; }

        public ServiceResponse(T value)
        {
            Value = value;
        }

    }
}
