using System.ComponentModel.DataAnnotations;

namespace Tangy_Models
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }

        //สินค้าที่สั่งซื้อ
        [Required]
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }

        //จำนวนและขนาดสินค้าที่สั่งซื้อ
        [Required]
        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public string ProductName { get; set; }
    }
}
