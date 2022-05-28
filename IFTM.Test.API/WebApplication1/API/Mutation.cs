using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.DTO;

namespace WebApplication1.GraphQL
{
    public class Mutation
    {
        public async Task<BookDto> UpsertBook([Service] TestContext context, BookDto bookDto)
        {
            var book = new Book
            {
                Author = bookDto.Author,
                Title = bookDto.Title,
                Price = bookDto.Price,
            };

            if (bookDto.Categories.Any())
            {
                var categories = new HashSet<string>(bookDto.Categories.Select(c => c.ToLower()));
                var alreadyAddedCategories = await context.Categories.Where(c => c.Name != null && categories.Contains(c.Name.ToLower())).ToListAsync();
                if (categories.Count != alreadyAddedCategories.Count)
                {
                    foreach (var category in categories)
                    {
                        if (!alreadyAddedCategories.Any(c => c.Name.ToLower() == category))
                        {
                            alreadyAddedCategories.Add(new Category
                            {
                                Name = category,
                            });
                        }
                    }
                }

                book.Categories = alreadyAddedCategories;
            }

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            return book.ToBookDTO();
        }
    }
}
