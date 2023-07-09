using Project_Managment_API.Model;

namespace Project_Managment_API.Interfaces
{
    public interface IOtpRepository
    {
        Task<OtpModel> GetOtpByEmailAsync(string email);
        System.Threading.Tasks.Task AddOtpAsync(OtpModel otp);

        System.Threading.Tasks.Task DeleteOtpModelAsync(OtpModel otpModel);


    }
}
