namespace Contracts.Http
{
    public enum ErrorCode
    {
        BadRequest = 40000,
        InvalidMessage = 40001,
        FileAlreadyExists = 40901,
        InvalidEmail = 42201,
        InvalidFile = 42202,
        InternalServerError = 50000
    }

    public class ErrorResponse
    {
        public ErrorCode Code { get; init; }
        public string Message { get; init; }
    }
}