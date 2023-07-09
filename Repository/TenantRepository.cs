using Microsoft.EntityFrameworkCore;
using Project_Management_API.Data;
using Project_Managment_API.DTO;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using Task = System.Threading.Tasks.Task;

namespace Project_Managment_API.Repository
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ProjectManagementDbContext _dbContext;

        public TenantRepository(ProjectManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tenant>> GetAllTenants()
        {
            var tenants = await _dbContext.Tenants.ToListAsync();
            return tenants;
        }

        public async Task<Tenant> GetTenantById(int tenantId)
        {
            var tenant = await _dbContext.Tenants.FindAsync(tenantId);
           
            return tenant;
        }

        public async Task CreateTenant(TenantDto tenantDto)
        {
            var tenant = new Tenant
            {
                Name = tenantDto.Name,
                // Map other properties accordingly
            };
            _dbContext.Tenants.Add(tenant);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateTenant(int tenantId, TenantDto tenantDto)
        {
            var tenant = await _dbContext.Tenants.FindAsync(tenantId);

            tenant.Name = tenantDto.Name;
            // Update other properties accordingly

            await _dbContext.SaveChangesAsync();
            
        }

        public async Task DeleteTenant(int tenantId)
        {
            var tenant = await _dbContext.Tenants.FindAsync(tenantId);

            _dbContext.Tenants.Remove(tenant);
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
