using Newtonsoft.Json;
using System.Text;
using Tangy_Client.Service.IService;
using Tangy_Models;

namespace Tangy_Client.Service
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<OrderDTO> Get(int orderHeaderId)
        {
            var response = await _httpClient.GetAsync($"/api/order/{orderHeaderId}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderDTO>(content);
                return order;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId)
        {
            var response = await _httpClient.GetAsync("/api/order");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(content);

                return orders;
            }

            return new List<OrderDTO>();
        }

        public async Task<OrderDTO> Create(StripePaymentDTO paymentDTO)
        {
            //แปลงให้อยู่ในรูปแบบ Json
            var content = JsonConvert.SerializeObject(paymentDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            //ส่งไปฝั่ง API และได้รับค่าที่ส่งกลับมา
            var response = await _httpClient.PostAsync("api/order/create", bodyContent);

            string responseResult = response.Content.ReadAsStringAsync().Result;

            //ที่ใช้รูปแบบนี้เพื่อให้สามารถตรวจสอบ error OK() จาก Controller
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderDTO>(responseResult);
                return result;
            }
            return new OrderDTO();
        }

        //ดำเนินการเปลี่ยนสถานะจาก Pending เป็น Confirmed
        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(OrderHeaderDTO orderHeader)
        {
            var content = JsonConvert.SerializeObject(orderHeader);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            //ส่งไปยัง Order Api เพื่อเปลี่ยนสถานะใน OrderHeaders database เป็น Confirmed
            var response = await _httpClient.PostAsync("api/order/paymentsuccessful", bodyContent);

            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderHeaderDTO>(responseResult);
                return result;
            }
            var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
            throw new Exception(errorModel.ErrorMessage);
        }

    }
}
