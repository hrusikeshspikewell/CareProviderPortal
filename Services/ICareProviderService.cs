using CareProviderPortal.dto;

namespace CareProviderPortal.Services
{
    public interface ICareProviderService
    {
        Task<IEnumerable<CareProviderDTO>> GetAllProviders();
        Task<CareProviderDTO> GetProviderById(int id);
        Task<CareProviderDTO> AddProvider(CareProviderCreateDTO careProvider);
        Task UpdateProvider(int id, CareProviderCreateDTO careProvider);
        Task DeleteProvider(int id);

        Task<IEnumerable<CareProviderDTO>> GetProvidersByDepartment(int departmentId);
        Task<IEnumerable<CareProviderDTO>> GetProvidersByExperience(int years);
    }
}
