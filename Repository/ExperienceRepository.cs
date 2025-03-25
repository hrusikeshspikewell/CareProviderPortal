using CareProviderPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CareProviderPortal.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly CareProviderPortalContext _context;
        public ExperienceRepository(CareProviderPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Experience>> GetAllExperiences()
        {
            return await _context.Experiences.ToListAsync();
        }

        public async Task<IEnumerable<Experience>> GetExperiencesByProviderId(int providerId)
        {
            return await _context.Experiences.Where(e => e.ProviderId == providerId).ToListAsync();
        }

        public async Task<Experience> GetById(int id)
        {
            return await _context.Experiences.FindAsync(id);
        }

        public async Task<Experience> Add(Experience entity)
        {
            var addedEntity = _context.Experiences.Add(entity);
            await _context.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task Update(Experience entity)
        {
            _context.Experiences.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
                await _context.SaveChangesAsync();
            }
        }
    }
}
