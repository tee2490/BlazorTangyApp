namespace Tangy_Client.Service.IService
{
    public interface IPaymentService
    {
        //return ผลลัพธ์เป็นสถานะสำเร็จหรือไม่
        public Task<SuccessModelDTO> Checkout(StripePaymentDTO model);
    }
}
