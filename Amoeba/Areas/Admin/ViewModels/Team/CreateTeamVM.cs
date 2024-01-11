using Amoeba.Models;
using System.ComponentModel.DataAnnotations;

namespace Amoeba.Areas.Admin.ViewModels
{
    public class CreateTeamVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string? TwitLink { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string? FaceLink { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string? InstaLink { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string? LinkedLink { get; set; }


        [Required(ErrorMessage = "Is Required")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public int PositionId { get; set; }

        public ICollection<Position>? Positions { get; set; }
    }
}
