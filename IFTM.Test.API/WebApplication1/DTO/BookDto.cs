using ContosoUniversity.Models;

namespace WebApplication1.DTO
{
    public class BookDto
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }

    public static class BookDtoExtensions
    {
        public static BookDto ToBookDTO(this Book book)
        {
            return new BookDto
            {
                Author = book.Author,
                Title = book.Title,
                Price = book.Price,
                Categories = book.Categories?.Select(c => c.Name?.ToLower()).ToList(),
            };
        }

        public static List<BookDto> ToBookDTO(this IEnumerable<Book> books)
        {
            if (books == null || !books.Any())
            {
                return new List<BookDto>();
            }

            return books.Select(b => b.ToBookDTO()).ToList();
        }
    }
}
