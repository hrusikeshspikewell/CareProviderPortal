using CareProviderPortal.dto;
using CareProviderPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareProviderPortal.Controllers
{
    [ApiController]
    [Route("api/experiences")]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;

        public ExperienceController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var experiences = await _experienceService.GetAllExperiences();
            return Ok(experiences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var experience = await _experienceService.GetExperienceById(id);
            if (experience == null)
                return NotFound();

            return Ok(experience);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExperienceCreateDTO experienceCreateDTO)
        {
            var createdExperience = await _experienceService.AddExperience(experienceCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdExperience.Id }, createdExperience);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExperienceCreateDTO experienceCreateDTO)
        {
            await _experienceService.UpdateExperience(id, experienceCreateDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _experienceService.DeleteExperience(id);
            return NoContent();
        }
    }
}
