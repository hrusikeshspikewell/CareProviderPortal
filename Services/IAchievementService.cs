using CareProviderPortal.dto;

namespace CareProviderPortal.Services
{
    public interface IAchievementService
    {
        Task<IEnumerable<AchievementDTO>> GetAllAchievements();
        Task<AchievementDTO> GetAchievementById(int id);
        Task<AchievementDTO> AddAchievement(AchievementCreateDTO achievement);
        Task UpdateAchievement(int id, AchievementCreateDTO achievement);
        Task DeleteAchievement(int id);
    }
}
