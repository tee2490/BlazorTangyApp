namespace Tangy_API.Helper
{
    //ค่าที่ใช้เป็นหล้กฐานจัดอยู่ในกลุ่มลายเซ็น Digital Signature
    public class APISettings
    {
        public string SecretKey { get; set; } //รหัสลับ
        public string ValidAudience { get; set; } //ส่งจากที่ไหน
        public string ValidIssuer { get; set; } //ส่งไปที่ไหน
    }
}
