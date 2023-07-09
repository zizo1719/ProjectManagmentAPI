using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Managment_API.DTO;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Managment_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/tenants")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantController(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Tenant>>> GetAllTenants()
        {
            var tenants = await _tenantRepository.GetAllTenants();
            return Ok(tenants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tenant>> GetTenantById(int id)
        {
            var tenant = await _tenantRepository.GetTenantById(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenant(TenantDto tenantDto)
        {
            await _tenantRepository.CreateTenant(tenantDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(int id, TenantDto tenantDto)
        {
            await _tenantRepository.UpdateTenant(id, tenantDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            await _tenantRepository.DeleteTenant(id);
            return Ok();
        }
    }
}
