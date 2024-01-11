using Amoeba.Models;
using System.ComponentModel.DataAnnotations;

namespace Amoeba.Areas.Admin.ViewModels
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Is Required")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public int CategoryId { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
