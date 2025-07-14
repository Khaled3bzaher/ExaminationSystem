namespace Domain.Exceptions
{
    public class BadRequestException(List<string> Errors) : Exception("Validation Failed")
    {
        public List<string> Errors { get; } = Errors;
    }
}
