using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManegmentSystem.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Title of Product")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Description of product")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [Display(Name ="Price of product")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name ="Items in stock")]
        public int Quantity {  get; set; }

        [Required]
        [Display(Name ="Image")]
        public string ImageFileName {  get; set; }

        [ForeignKey("Category")]
        public int? CategoryId {  get; set; }

        public virtual Category Category { get; set; }
    }
}
