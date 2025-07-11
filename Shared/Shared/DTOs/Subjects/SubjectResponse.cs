namespace Shared.DTOs.Subjects
{
    public record SubjectResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
