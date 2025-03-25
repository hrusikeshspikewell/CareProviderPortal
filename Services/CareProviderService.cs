using AutoMapper;
using CareProviderPortal.dto;
using CareProviderPortal.Models;
using CareProviderPortal.Repository;
using Microsoft.EntityFrameworkCore;

namespace CareProviderPortal.Services
{
    public class CareProviderService : ICareProviderService
    {
        private readonly IRepository<CareProvider> _repository;
        private readonly IMapper _mapper;
        private readonly CareProviderPortalContext _context;

        public CareProviderService(IRepository<CareProvider> repository, IMapper mapper, CareProviderPortalContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CareProviderDTO>> GetAllProviders() => _mapper.Map<IEnumerable<CareProviderDTO>>(await _repository.GetAll());

        public async Task<CareProviderDTO> GetProviderById(int id)
        {
            var provider = await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<CareProviderDTO>(provider);
        }

        public async Task<CareProviderDTO> AddProvider(CareProviderCreateDTO careProviderDTO)
        {
            var careProvider = _mapper.Map<CareProvider>(careProviderDTO);
            var createdProvider = await _repository.Add(careProvider);
            return _mapper.Map<CareProviderDTO>(createdProvider);
        }

        public async Task UpdateProvider(int id, CareProviderCreateDTO careProviderDTO)
        {
            var existingProvider = await _repository.GetById(id);
            if (existingProvider == null)
            {
                throw new Exception("Provider not found.");
            }
            existingProvider.Name = careProviderDTO.Name;
            existingProvider.Email = careProviderDTO.Email;
            existingProvider.Phone = careProviderDTO.Phone;
            existingProvider.Specialization = careProviderDTO.Specialization;
            existingProvider.DepartmentId = careProviderDTO.DepartmentId;
            existingProvider.Status = careProviderDTO.Status;

            await _repository.Update(existingProvider);
        }

        public async Task DeleteProvider(int id)
        {
            var provider = await _repository.GetById(id);
            if (provider != null)
            {
                provider.Status = "LEFT";
                await _repository.Update(provider);
            }
        }

        // New method: Get providers by department
        public async Task<IEnumerable<CareProviderDTO>> GetProvidersByDepartment(int departmentId)
        {
            var providers = await _context.CareProviders
                .Where(p => p.DepartmentId == departmentId)
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CareProviderDTO>>(providers);
        }

        public async Task<IEnumerable<CareProviderDTO>> GetProvidersByExperience(int years)
        {
            var providers = await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();

            var filteredProviders = providers.Where(p =>
                p.Experiences.Any(e =>
                {
                    var start = e.StartDate.ToDateTime(new TimeOnly(0, 0));
                    var end = e.EndDate.HasValue ? e.EndDate.Value.ToDateTime(new TimeOnly(0, 0)) : DateTime.Now;
                    return (end - start).TotalDays / 365 >= years;
                }));

            return _mapper.Map<IEnumerable<CareProviderDTO>>(filteredProviders);
        }
    }
}
