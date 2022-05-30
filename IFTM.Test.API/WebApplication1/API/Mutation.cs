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
            var book = await context.Books.Include(b => b.Categories)
                                          .FirstOrDefaultAsync(b => b.Author == bookDto.Author && b.Title == bookDto.Title);
            bool isUpdate = false;
            if (book != null)
            {
                isUpdate = true;
                book.Price = bookDto.Price;
            }
            else
            {
                book = new Book
                {
                    Author = bookDto.Author,
                    Title = bookDto.Title,
                    Price = bookDto.Price,
                };
            }

            if (bookDto.Categories.Any())
            {
                var categories = new HashSet<string>(bookDto.Categories.Select(c => c.ToLower()));
                var alreadyAddedCategories = await context.Categories.Where(c => c.Name != null && categories.Contains(c.Name.ToLower())).ToListAsync();
                var newCategories = new List<Category>();
                if (categories.Count != alreadyAddedCategories.Count)
                {
                    foreach (var category in categories)
                    {
                        if (!alreadyAddedCategories.Any(c => c.Name.ToLower() == category))
                        {
                            newCategories.Add(new Category
                            {
                                Name = category,
                            });
                        }
                    }
                }

                if (isUpdate)
                {
                    var removedCategories = book.Categories.Where(c => !categories.Contains(c.Name.ToLower())).ToList();
                    if (removedCategories.Any())
                    {
                        foreach (var category in removedCategories)
                        {
                            book.Categories.Remove(category);
                        }
                    }

                    var addCategories = alreadyAddedCategories.Where(c => !book.Categories.Contains(c)).ToList();
                    newCategories = newCategories.Concat(addCategories).ToList();
                    if (newCategories.Any())
                    {
                        foreach (var item in newCategories)
                        {
                            book.Categories.Add(item);
                        }
                    }
                }
                else
                {
                    book.Categories = alreadyAddedCategories.Concat(newCategories).ToList();
                }
            }

            if (!isUpdate)
            {
                await context.Books.AddAsync(book);
            }
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            return book.ToBookDTO();
        }
    }
}
