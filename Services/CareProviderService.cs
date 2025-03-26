using AutoMapper;
using CareProviderPortal.dto;
using CareProviderPortal.Models;
using CareProviderPortal.Repository;
using Microsoft.EntityFrameworkCore;

namespace CareProviderPortal.Services
{
    public class CareProviderService : ICareProviderService
    {
        private readonly ICareProviderRepository _repository;
        private readonly IMapper _mapper;

        public CareProviderService(ICareProviderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CareProviderDTO>> GetAllProviders()
        {
            var providers = await _repository.GetAll();
            return _mapper.Map<IEnumerable<CareProviderDTO>>(providers);
        }

        public async Task<CareProviderDTO> GetProviderById(int id)
        {
            var provider = await _repository.GetById(id);
            return _mapper.Map<CareProviderDTO>(provider);
        }

        public async Task<CareProviderDTO> AddProvider(CareProviderCreateDTO careProviderDTO)
        {
            var provider = _mapper.Map<CareProvider>(careProviderDTO);
            var createdProvider = await _repository.Add(provider);
            return _mapper.Map<CareProviderDTO>(createdProvider);
        }

        public async Task UpdateProvider(int id, CareProviderCreateDTO careProviderDTO)
        {
            var existingProvider = await _repository.GetById(id);
            if (existingProvider != null)
            {
                _mapper.Map(careProviderDTO, existingProvider);
                await _repository.Update(existingProvider);
            }

            
        }

        public async Task DeleteProvider(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<CareProviderDTO>> GetProvidersByDepartment(int departmentId)
        {
            var providers = await _repository.GetProvidersByDepartment(departmentId);
            return _mapper.Map<IEnumerable<CareProviderDTO>>(providers);
        }

        public async Task<IEnumerable<CareProviderDTO>> GetProvidersByExperience(int years)
        {
            var providers = await _repository.GetProvidersByExperience(years);
            return _mapper.Map<IEnumerable<CareProviderDTO>>(providers);
        }
    }
}
