using System.ComponentModel.DataAnnotations;

namespace Tangy_DataAccess
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        // add navigation property : #TODO

        //ส่วนสรุปการสั่งซื้อสินค้า
        [Required]
        public double OrderTotal { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime ShippingDate { get; set; }
        [Required]
        public string Status { get; set; }

        //stripe payment รหัสและสถานะการชำระเงิน
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        //ข้อมูลทั่วไปของลูกค้า
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Tracking { get; set; } //หมายเลขการจัดส่ง
        public string? Carrier { get; set; }  //วิธีการจัดส่ง เช่น kerry
    }
}
