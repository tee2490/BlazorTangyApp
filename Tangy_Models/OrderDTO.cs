namespace Tangy_Models
{
    public class OrderDTO
    {
        public OrderHeaderDTO OrderHeader { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
