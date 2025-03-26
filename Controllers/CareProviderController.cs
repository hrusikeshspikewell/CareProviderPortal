using CareProviderPortal.dto;
using CareProviderPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareProviderPortal.Controllers
{
    [ApiController]
    [Route("api/careproviders")]
    public class CareProviderController : ControllerBase
    {
        private readonly ICareProviderService _service;
        public CareProviderController(ICareProviderService service) 
        { 
            _service = service; 
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllProviders());

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(int id) 
        { 
            var provider = await _service.GetProviderById(id); 
            return provider == null ? NotFound() : Ok(provider); 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CareProviderCreateDTO providerDTO)
        {
            var createdProvider = await _service.AddProvider(providerDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdProvider.Id }, createdProvider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CareProviderCreateDTO providerDTO)
        {
            await _service.UpdateProvider(id, providerDTO);
            return Ok();
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> Delete(int id) 
        { 
            await _service.DeleteProvider(id); 
            return Ok();
        }
        
        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var providers = await _service.GetProvidersByDepartment(departmentId);
            return Ok(providers);
        }

        // New API: Get providers by experience
        [HttpGet("experience/{years}")]
        public async Task<IActionResult> GetByExperience(int years)
        {
            var providers = await _service.GetProvidersByExperience(years);
            return Ok(providers);
        }
    }

}
