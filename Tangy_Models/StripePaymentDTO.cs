namespace Tangy_Models
{
    public class StripePaymentDTO
    {
        public StripePaymentDTO()
        {
            SuccessUrl = "OrderConfirmation";
            CancelUrl = "Summary";
        }

        public OrderDTO Order { get; set; }

        //SuccessUrl และ CancelUrl ใช้แนบข้อความให้กับ url สำหรับ redirect ผลลัพธ์สำเร็จหรือไม่
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
