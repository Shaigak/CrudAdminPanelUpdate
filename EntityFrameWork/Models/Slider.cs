using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameWork.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }


        [NotMapped]
        [Required(ErrorMessage ="Dont Be Empty")]
        public IFormFile Photo { get; set; } 

    }
}
