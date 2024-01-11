using System.ComponentModel.DataAnnotations;

namespace Amoeba.Areas.Admin.ViewModels
{
    public class UpdateServiceVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string Icon { get; set; }
    }
}
