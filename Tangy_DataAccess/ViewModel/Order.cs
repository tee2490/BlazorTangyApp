namespace Tangy_DataAccess.ViewModel
{
    public class Order
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
