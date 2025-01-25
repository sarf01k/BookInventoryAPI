using System.ComponentModel.DataAnnotations;

namespace BookInventoryAPI.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Book title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Book author is required")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Book price is required")]
        public decimal Price { get; set; }
        public string Genre { get; set; }
    }
}