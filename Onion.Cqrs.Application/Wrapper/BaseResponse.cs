namespace Onion.Cqrs.Application.Wrapper
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
