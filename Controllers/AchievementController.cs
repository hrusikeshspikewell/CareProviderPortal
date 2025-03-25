using CareProviderPortal.dto;
using CareProviderPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareProviderPortal.Controllers
{
    [ApiController]
    [Route("api/achievements")]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementService _service;
        public AchievementController(IAchievementService service) { _service = service; }

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAchievements());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var achievement = await _service.GetAchievementById(id);
            return achievement == null ? NotFound() : Ok(achievement);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AchievementCreateDTO achievementDTO)
        {
            var createdAchievement = await _service.AddAchievement(achievementDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdAchievement.Id }, createdAchievement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AchievementCreateDTO achievementDTO)
        {
            await _service.UpdateAchievement(id, achievementDTO);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAchievement(id);
            return NoContent();
        }
    }

}
