using AutoMapper;
using CareProviderPortal.dto;
using CareProviderPortal.Models;
using CareProviderPortal.Repository;

namespace CareProviderPortal.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _repository;
        private readonly IMapper _mapper;
        public AchievementService(IAchievementRepository repository, IMapper mapper) 
        { 
            _repository = repository; 
            _mapper = mapper; 
        }

        public async Task<IEnumerable<AchievementDTO>> GetAllAchievements() => _mapper.Map<IEnumerable<AchievementDTO>>(await _repository.GetAll());
        public async Task<AchievementDTO> GetAchievementById(int id) => _mapper.Map<AchievementDTO>(await _repository.GetById(id));
        public async Task<AchievementDTO> AddAchievement(AchievementCreateDTO achievementDTO)
        {
            var achievement = _mapper.Map<Achievement>(achievementDTO);
            var createdAchievement = await _repository.Add(achievement);
            return _mapper.Map<AchievementDTO>(createdAchievement);
        }

        public async Task UpdateAchievement(int id, AchievementCreateDTO achievementDTO)
        {
            var existingAchievement = await _repository.GetById(id);
            if (existingAchievement == null)
            {
                throw new Exception("Achievement not found.");
            }

            existingAchievement.ProviderId = achievementDTO.ProviderId;
            existingAchievement.Title = achievementDTO.Title;
            existingAchievement.Description = achievementDTO.Description;
            existingAchievement.AchievedDate = achievementDTO.AchievedDate;

            await _repository.Update(existingAchievement);
        }
        public async Task DeleteAchievement(int id) 
        { 
            await _repository.Delete(id); 
        }
    }

}
