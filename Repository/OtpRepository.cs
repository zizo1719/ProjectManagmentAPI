using Microsoft.EntityFrameworkCore;
using Project_Management_API.Data;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;

namespace Project_Managment_API.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly ProjectManagementDbContext _dbContext;

        public OtpRepository(ProjectManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  async System.Threading.Tasks.Task AddOtpAsync(OtpModel otpModel)
        {

            _dbContext.OtpModels.Add(otpModel);
            await _dbContext.SaveChangesAsync();

        }


        public async System.Threading.Tasks.Task DeleteOtpModelAsync(OtpModel otpModel)
        {
            _dbContext.OtpModels.Remove(otpModel);
            await _dbContext.SaveChangesAsync();
        }

            public async Task<OtpModel> GetOtpByEmailAsync(string email)
        {
            return await _dbContext.OtpModels.FirstOrDefaultAsync(otp => otp.Email == email);
        }

       
    }
}
