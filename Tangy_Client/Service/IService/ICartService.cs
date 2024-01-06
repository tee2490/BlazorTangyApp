using Tangy_Client.ViewModels;

namespace Tangy_Client.Service.IService
{
    //สามารถเพิ่มลด จำนวนสินค้าในตะกร้าได้
    public interface ICartService
    {
        public event Action OnChange;
        Task DecrementCart(ShoppingCart shoppingCart);
        Task IncrementCart(ShoppingCart shoppingCart);
    }
}
