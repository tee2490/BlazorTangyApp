
using System.ComponentModel.DataAnnotations;

namespace Tangy_Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name..")]
        public string Name { get; set; }
    }
}
