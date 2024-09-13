using System.ComponentModel.DataAnnotations;

namespace ProductManegmentSystem.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="The Category name is required")]
        [Display(Name ="Category Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Category Description")]
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
