using Project_Managment_API.DTO;
using Project_Managment_API.Model;
using Task = System.Threading.Tasks.Task;

namespace Project_Managment_API.Interfaces
{
    public interface ITenantRepository
    {
        Task<Tenant> GetTenantById(int tenantId);
        Task<IEnumerable<Tenant>> GetAllTenants();
        Task CreateTenant(TenantDto tenantDto);
        Task UpdateTenant(int tenantId, TenantDto tenantDto);
        Task DeleteTenant(int tenantId);

    }
}
