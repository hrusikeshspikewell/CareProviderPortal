using CareProviderPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CareProviderPortal.Repository
{
    public class AchievementRepository : IRepository<Achievement>
    {
        private readonly CareProviderPortalContext _context;
        public AchievementRepository(CareProviderPortalContext context) 
        { 
            _context = context; 
        }

        public async Task<IEnumerable<Achievement>> GetAll() => await _context.Achievements.ToListAsync();
        public async Task<Achievement> GetById(int id) => await _context.Achievements.FindAsync(id);
        public async Task<Achievement> Add(Achievement entity)
        {
            var addedEntity = _context.Achievements.Add(entity);
            await _context.SaveChangesAsync();
            return addedEntity.Entity;
        }
        public async Task Update(Achievement entity) 
        { 
            _context.Achievements.Update(entity); 
            await _context.SaveChangesAsync(); 
        }
        public async Task Delete(int id)
        {
            var achievement = await _context.Achievements.FindAsync(id);
            if (achievement != null) { _context.Achievements.Remove(achievement); await _context.SaveChangesAsync(); }
        }
    }

}
