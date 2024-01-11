using System.ComponentModel.DataAnnotations;

namespace Amoeba.Areas.Admin.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string UserNameOrPassword { get; set; }


        [Required(ErrorMessage = "Is Required")]
        public string Password { get; set; }

    }
}
