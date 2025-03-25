using CareProviderPortal.Models;

namespace CareProviderPortal.Repository
{
    public interface IExperienceRepository
    {
        Task<IEnumerable<Experience>> GetAllExperiences();
        Task<IEnumerable<Experience>> GetExperiencesByProviderId(int providerId);
        Task<Experience> GetById(int id);
        Task<Experience> Add(Experience entity);
        Task Update(Experience entity);
        Task Delete(int id);
    }
}