namespace Project_Managment_API.Model
{
    public class OtpModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
