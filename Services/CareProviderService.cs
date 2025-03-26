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

        // Use _context with Include to ensure Experiences and Achievements are loaded
        public async Task<IEnumerable<CareProviderDTO>> GetAllProviders()
        {
            var providers = await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();

            var dtos = _mapper.Map<IEnumerable<CareProviderDTO>>(providers);
            foreach (var dto in dtos)
            {
                var provider = providers.FirstOrDefault(p => p.Id == dto.Id);
                if (provider != null)
                {
                    dto.TotalExperienceYears = CalculateTotalExperience(provider.Experiences);
                }
            }
            return dtos;
        }

        // Use _context with Include for detailed data
        public async Task<CareProviderDTO> GetProviderById(int id)
        {
            var provider = await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .FirstOrDefaultAsync(p => p.Id == id);

            var dto = _mapper.Map<CareProviderDTO>(provider);
            if (provider != null)
            {
                dto.TotalExperienceYears = CalculateTotalExperience(provider.Experiences);
            }
            return dto;
        }

        public async Task<CareProviderDTO> AddProvider(CareProviderCreateDTO careProviderDTO)
        {
            var careProvider = _mapper.Map<CareProvider>(careProviderDTO);
            var createdProvider = await _repository.Add(careProvider);
            // Reload experiences if needed using _context, or assume none exist on creation.
            var dto = _mapper.Map<CareProviderDTO>(createdProvider);
            dto.TotalExperienceYears = CalculateTotalExperience(createdProvider.Experiences);
            return dto;
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

        public async Task<IEnumerable<CareProviderDTO>> GetProvidersByDepartment(int departmentId)
        {
            var providers = await _context.CareProviders
                .Where(p => p.DepartmentId == departmentId)
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();

            var dtos = _mapper.Map<IEnumerable<CareProviderDTO>>(providers);
            foreach (var dto in dtos)
            {
                var provider = providers.FirstOrDefault(p => p.Id == dto.Id);
                if (provider != null)
                {
                    dto.TotalExperienceYears = CalculateTotalExperience(provider.Experiences);
                }
            }
            return dtos;
        }

        public async Task<IEnumerable<CareProviderDTO>> GetProvidersByExperience(int years)
        {
            var providers = await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();

            var filteredProviders = providers.Where(p =>
                CalculateTotalExperience(p.Experiences) >= years);

            var dtos = _mapper.Map<IEnumerable<CareProviderDTO>>(filteredProviders);
            foreach (var dto in dtos)
            {
                var provider = filteredProviders.FirstOrDefault(p => p.Id == dto.Id);
                if (provider != null)
                {
                    dto.TotalExperienceYears = CalculateTotalExperience(provider.Experiences);
                }
            }
            return dtos;
        }

        // Helper method: Sums the total days across all experiences and converts to years.
        private int CalculateTotalExperience(ICollection<Experience> experiences)
        {
            double totalDays = experiences.Sum(exp =>
            {
                var start = exp.StartDate;  // Assuming StartDate is DateTime
                var end = exp.EndDate.HasValue ? exp.EndDate.Value : DateTime.Now;
                return (end - start).TotalDays;
            });
            return (int)Math.Floor(totalDays / 365.25);
        }
    }
}
