namespace DotNet9API.Models
{
    public class Book
    {
        public required Guid Id { get; init; }
        public required string Title { get; set; }
        public required string Isbn { get; init; }
    }
}
