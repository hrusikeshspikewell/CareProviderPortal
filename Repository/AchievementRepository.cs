using CareProviderPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CareProviderPortal.Repository
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly CareProviderPortalContext _context;

        public AchievementRepository(CareProviderPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Achievement>> GetAll()
        {
            return await _context.Achievements.ToListAsync();
        }

        public async Task<Achievement> GetById(int id)
        {
            return await _context.Achievements.FindAsync(id);
        }

        public async Task<Achievement> Add(Achievement achievement)
        {
            await _context.Achievements.AddAsync(achievement);
            await _context.SaveChangesAsync();
            return achievement;
        }

        public async Task Update(Achievement achievement)
        {
            _context.Achievements.Update(achievement);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var achievement = await _context.Achievements.FindAsync(id);
            if (achievement != null)
            {
                _context.Achievements.Remove(achievement);
                await _context.SaveChangesAsync();
            }
        }
    }
}