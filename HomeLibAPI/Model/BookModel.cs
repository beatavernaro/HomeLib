namespace HomeLibAPI.Model
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Authors { get; set; } = string.Empty;
        public string PublishedDate { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public BookModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
