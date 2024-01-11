using System.ComponentModel.DataAnnotations;

namespace Amoeba.Areas.Admin.ViewModels
{
    public class CreateSettingsVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string Value { get; set; }
    }
}
