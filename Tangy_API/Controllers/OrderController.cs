﻿using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tangy_Models;

namespace Tangy_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll());
        }

        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            if (orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var orderDTO = await _orderRepository.Get(orderHeaderId.Value);
            if (orderDTO == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(orderDTO);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            paymentDTO.Order.OrderHeader.OrderDate = DateTime.Now;

            var result = await _orderRepository.Create(paymentDTO.Order);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("paymentsuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDTO orderHeaderDTO)
        {
            var service = new SessionService(); //using Stripe.Checkout
            var sessionDetails = service.Get(orderHeaderDTO.SessionId); //ไปอ่านมาจาก stripe API

            if (sessionDetails.PaymentStatus == "paid")
            {
                //ไปเปลี่ยนสถานะจาก Pending เป็น Confirmed และบันทึก PaymentIntentId(ID ติดตามการเบิกจ่าย) ใน database
                var result = await _orderRepository.MarkPaymentSuccessful(orderHeaderDTO.Id, sessionDetails.PaymentIntentId);

                if (result == null)
                {
                    return BadRequest(new ErrorModelDTO()
                    {
                        ErrorMessage = "Can not mark payment as successful"
                    });
                }
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
