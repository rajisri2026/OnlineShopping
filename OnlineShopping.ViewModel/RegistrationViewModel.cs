using OnlineShopping.DomainLayer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OnlineShopping.ViewModel
{
    public class RegistrationViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^([A-Z]{1})[a-z]+", ErrorMessage = "Invalid name")]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"^([A-Z]{1})[a-z]+", ErrorMessage = "Invalid name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"(^[a-z]+\.[a-z]+@[a-z]+\.(com|net|edu)$)|(^[a-z]+[0-9]*@[a-z]+\.(com|net|edu))", ErrorMessage = "Invalid Email id")]
        [Display(Name = "Email Id")]
        [Remote("ExistingUserEmail", "Account", ErrorMessage = "Email id already exists")]
        public string UserEmail { get; set; }
        [Required]
        [RegularExpression(@"^(?=.{5,20}$)(([a-z0-9])\2?(?!\2))+$", ErrorMessage = "Invalid username")]
        [Remote("ExistingUsername","Account",ErrorMessage ="Username already exists")]
       public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
      //  [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])(?=.*[\S]).{6,15}$", ErrorMessage = "Password must be between 6 and 15 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Display(Name = "Re-type Password")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }

        public Role Role { get; set; }

    }
}
