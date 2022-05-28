using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.DTO;

namespace WebApplication1.GraphQL
{
    public class Query
    {
        public async Task<List<BookDto>> GetBooks([Service] TestContext context, string category)
        {
            category = category.ToLower();
            if (string.IsNullOrWhiteSpace(category))
            {
                var result = await context.Books.Include(c => c.Categories).ToListAsync();
                return result.ToBookDTO();
            }


            var books = await context.Books
                .Where(b => b.Categories.Any(c => c.Name != null &&
                                                  c.Name.ToLower() == category))
                .Include(b => b.Categories)
                .ToListAsync();
            return books.ToBookDTO();
        }
    }
}
