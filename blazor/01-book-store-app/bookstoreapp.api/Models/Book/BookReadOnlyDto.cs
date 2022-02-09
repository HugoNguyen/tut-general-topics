namespace bookstoreapp.api.Models.Book
{
    public class BookReadOnlyDto:BaseDto
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public int AuthorName { get; set; }
    }
}
