using AutoMapper;
using CareProviderPortal.dto;
using CareProviderPortal.Models;
using CareProviderPortal.Repository;

namespace CareProviderPortal.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _repository;
        private readonly IMapper _mapper;

        public ExperienceService(IExperienceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExperienceDTO>> GetAllExperiences()
        {
            return _mapper.Map<IEnumerable<ExperienceDTO>>(await _repository.GetAllExperiences());
        }

        public async Task<IEnumerable<ExperienceDTO>> GetExperiencesByProviderId(int providerId)
        {
            return _mapper.Map<IEnumerable<ExperienceDTO>>(await _repository.GetExperiencesByProviderId(providerId));
        }

        public async Task<ExperienceDTO> GetExperienceById(int id)
        {
            return _mapper.Map<ExperienceDTO>(await _repository.GetById(id));
        }

        public async Task<ExperienceDTO> AddExperience(ExperienceCreateDTO experienceDTO)
        {
            var experience = _mapper.Map<Experience>(experienceDTO);
            var createdExperience = await _repository.Add(experience);
            return _mapper.Map<ExperienceDTO>(createdExperience);
        }

        public async Task UpdateExperience(int id, ExperienceCreateDTO experienceDTO)
        {
            var existingExperience = await _repository.GetById(id);
            if (existingExperience == null) throw new Exception("Experience not found.");

            existingExperience.Organization = experienceDTO.Organization;
            existingExperience.Position = experienceDTO.Position;
            existingExperience.StartDate = experienceDTO.StartDate;
            existingExperience.EndDate = experienceDTO.EndDate;
            existingExperience.Description = experienceDTO.Description;

            await _repository.Update(existingExperience);
        }

        public async Task DeleteExperience(int id)
        {
            await _repository.Delete(id);
        }
    }
}
