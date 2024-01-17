using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tangy_Models;

namespace Tangy_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost, Authorize]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            try
            {
                var domain = _configuration.GetValue<string>("Tangy_Client_URL");

                //session stripe .net : https://stripe.com/docs/api/checkout/sessions/create?lang=dotnet
                //stripe-hosted page : https://stripe.com/docs/checkout/quickstart?lang=dotnet

                //สร้างข้อมูลตามแบบฟอร์มของ stripe 
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + paymentDTO.SuccessUrl,
                    CancelUrl = domain + paymentDTO.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                foreach (var item in paymentDTO.Order.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100), //20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService(); //สร้างออบเจคสำหรับขอใช้บริการของ stripe   
                Session session = service.Create(options); //ส่งไปยัง stripe API

                return Ok(new SuccessModelDTO()
                {
                    Data = session.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
