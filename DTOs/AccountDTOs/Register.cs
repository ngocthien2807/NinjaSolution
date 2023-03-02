using Obj_Common;
using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountDTOs
{
    public class Register
    {
        [MaxLength(50), Required] public string Name { get; set; }
        [MaxLength(20), Required] public string Username { get; set; }
        [MaxLength(20), Required] public string Password { get; set; }

        [MaxLength(20), Required]
        [Compare(nameof(Password))] 
        public string ConfirmPassword { get; set; }

        public Role Role { get; set; }
        public string Avatar { get; set; }
    }
}
