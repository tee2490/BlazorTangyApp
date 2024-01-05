using System.ComponentModel.DataAnnotations;
using Tangy_Models;

namespace Tangy_Client.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            ProductPrice = new();
            Count = 1;
        }

        [Required]
        public int SelectedProductPriceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public int Count { get; set; }
        
        public ProductPriceDTO ProductPrice { get; set; }
    }
}
