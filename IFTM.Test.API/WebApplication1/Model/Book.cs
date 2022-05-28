using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
