using CareProviderPortal.Models;

namespace CareProviderPortal.Repository
{
    public interface ICareProviderRepository
    {
        Task<IEnumerable<CareProvider>> GetAll();
        Task<CareProvider> GetById(int id);
        Task<CareProvider> Add(CareProvider entity);
        Task Update(CareProvider entity);
        Task Delete(int id);
        Task<IEnumerable<CareProvider>> GetProvidersByDepartment(int departmentId);
        Task<IEnumerable<CareProvider>> GetProvidersByExperience(int years);
    }
}
