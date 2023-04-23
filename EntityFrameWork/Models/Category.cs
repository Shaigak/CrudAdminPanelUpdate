using System.ComponentModel.DataAnnotations;

namespace EntityFrameWork.Models
{
    public class Category:BaseEntity
    {
        [Required(ErrorMessage ="Dony be Empty ")]
        [StringLength(10,ErrorMessage ="The name length is not corrext")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
