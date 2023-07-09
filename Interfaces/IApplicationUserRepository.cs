using Project_Managment_API.Model;

namespace Project_Managment_API.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> CreateUser(ApplicationUser user, string password);
        Task<bool> CheckPassword(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByEmail(string username);
        Task<bool> UserExists(string username);
        Task<ApplicationUser> GetUserById(string userId);
        System.Threading.Tasks.Task ResetPassword(ApplicationUser user, string newPassword);



    }
}
