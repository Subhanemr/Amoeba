using System.ComponentModel.DataAnnotations;

namespace Amoeba.Areas.Admin.ViewModels
{
    public class CreatePositionVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string Name { get; set; }
    }
}
