namespace RoaylLibrary.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int TotalCopies { get; set; }
        public int CopiesInUse { get; set; }
        public required string EbookType { get; set; }
        public required string Isbn { get; set; }
        public required string Category { get; set; }
        public required string Publisher { get; set; }
    }
}
