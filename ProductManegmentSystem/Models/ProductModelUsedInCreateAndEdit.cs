using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManegmentSystem.Models
{
    public class ProductModelUsedInCreateAndEdit
    {
        [Required]
        [Display(Name = "Title of Product")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description of product")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [Display(Name = "Price of product")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Items in stock")]
        public int Quantity { get; set; }

        
        [Display(Name = "Image")]
        public IFormFile ? ImageFile { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
