
namespace TravelBooking.Common.Exceptions;

public class ValidationExceptionList : Exception
{
    public new string? Message { get; }
    public List<string> Errors { get; }

    public ValidationExceptionList(List<string> errors)
    {
        Errors = errors;
    }
    public ValidationExceptionList(string message, List<string> errors)
    {
        Message = message;
        Errors = errors;
    }
}