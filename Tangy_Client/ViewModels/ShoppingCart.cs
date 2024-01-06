using Tangy_Models;

namespace Tangy_Client.ViewModels
{
    //จะซื้อสินค้าอะไร ขนาดเท่าไหร่
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int ProductPriceId { get; set; }
        public ProductPriceDTO ProductPrice { get; set; }
        public int Count { get; set; }
    }
}
