using CareProviderPortal.Models;

namespace CareProviderPortal.Repository
{
    public interface IAchievementRepository
    {
        Task<IEnumerable<Achievement>> GetAll();
        Task<Achievement> GetById(int id);
        Task<Achievement> Add(Achievement achievement);
        Task Update(Achievement achievement);
        Task Delete(int id);
    }
}
