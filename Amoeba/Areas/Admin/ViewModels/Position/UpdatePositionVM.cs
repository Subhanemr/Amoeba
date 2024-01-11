using System.ComponentModel.DataAnnotations;

namespace Amoeba.Areas.Admin.ViewModels
{
    public class UpdatePositionVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string Name { get; set; }
    }
}
