using System.ComponentModel.DataAnnotations;

namespace Amoeba.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Not compare with Password")]
        public string ConfirmedPassword { get; set; }
    }
}
