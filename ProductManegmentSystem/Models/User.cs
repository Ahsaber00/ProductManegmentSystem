using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManegmentSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10,MinimumLength =3,ErrorMessage ="The First Name must be between 3 and 10")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; } 

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The last Name must be between 3 and 10")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Adress")]
        public string Adress {  get; set; }
        
        [Required]
        [Display(Name ="Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display (Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage ="The Password and the confirm password do not match")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword {  get; set; }
    }
}
