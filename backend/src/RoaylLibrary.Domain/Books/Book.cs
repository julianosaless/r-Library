namespace RoaylLibrary.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
        public int CopiesInUse { get; set; }
        public string BookType { get; set; }
        public string Isbn { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }
    }
}
