using CareProviderPortal.dto;

namespace CareProviderPortal.Services
{
    public interface IExperienceService
    {
        Task<IEnumerable<ExperienceDTO>> GetAllExperiences();
        Task<IEnumerable<ExperienceDTO>> GetExperiencesByProviderId(int providerId);
        Task<ExperienceDTO> GetExperienceById(int id);
        Task<ExperienceDTO> AddExperience(ExperienceCreateDTO experience);
        Task UpdateExperience(int id, ExperienceCreateDTO experience);
        Task DeleteExperience(int id);
    }
}
