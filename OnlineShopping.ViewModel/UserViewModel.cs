


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineShopping.DomainLayer;
namespace OnlineShopping.ViewModel
{
    public class UserViewModel
    {
       [Key]
        public int UserId { get; set; }
        //[Required]
        //[Display(Name = "First Name")]
        //[StringLength(50,MinimumLength =3)]
        //[RegularExpression(@"^([A-Z]{1})[a-z]+", ErrorMessage = "Numbers not allowed")]
        public string FirstName { get; set; }

        //[StringLength(20, MinimumLength = 1)]
        //[Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Required]
      //  [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:dd-MM-yyyy}")]
        //[DataType(DataType.Date)]
        //public DateTime DateOfBirth { get; set; }

        //[Required]
        //[RegularExpression(@"(^[a-z]+\.[a-z]+@[a-z]+\.(com|net|edu)$)|(^[a-z]+[0-9]*@[a-z]+\.(com|net|edu))", ErrorMessage = "Invalid Email id")]
        //[Display(Name = "Email Id")]
        public string UserEmail { get; set; }

        //[Required]
        //[Display(Name = "Phone Number")]
        //[RegularExpression(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)",ErrorMessage ="Invalid phone number")]
       // public string PhoneNumber { get; set; }

        //[Required]
        //[RegularExpression(@"^(?=.{5,20}$)(([a-z0-9])\2?(?!\2))+$", ErrorMessage = "Invalid username")]
        public string UserName { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

      
        public int RoleId { get; set; }

        
        public virtual Role Role { get; set; }

    }
}
