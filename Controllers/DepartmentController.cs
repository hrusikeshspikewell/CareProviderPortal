using CareProviderPortal.dto;
using CareProviderPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareProviderPortal.Controllers
{
    [Route("api/departments")]
    [ApiController]

    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service) { _service = service; }

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllDepartments());
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _service.GetDepartmentById(id);
            return department == null ? NotFound() : Ok(department);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDTO departmentCreateDTO)
        {
            var createdDepartment = await _service.AddDepartment(departmentCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdDepartment.Id }, createdDepartment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentCreateDTO departmentCreateDTO)
        {
            await _service.UpdateDepartment(id, departmentCreateDTO);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteDepartment(id);
            return Ok();
        }
    }

}
