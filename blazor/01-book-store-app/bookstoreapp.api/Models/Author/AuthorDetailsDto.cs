using bookstoreapp.api.Models.Book;

namespace bookstoreapp.api.Models.Author
{
    public class AuthorDetailsDto : AuthorReadOnlyDto
    {
        public List<BookReadOnlyDto> Books { get; set; }
    }
}
