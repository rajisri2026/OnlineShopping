
using System.ComponentModel.DataAnnotations;
using OnlineShopping.DomainLayer;
namespace OnlineShopping.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
