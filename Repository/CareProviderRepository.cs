using CareProviderPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CareProviderPortal.Repository
{
    public class CareProviderRepository : IRepository<CareProvider>
    {
        private readonly CareProviderPortalContext _context;
        public CareProviderRepository(CareProviderPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CareProvider>> GetAll()
            => await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();

        public async Task<CareProvider> GetById(int id)
            => await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<CareProvider> Add(CareProvider entity)
        {
            await _context.CareProviders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(CareProvider entity)
        {
            _context.CareProviders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var provider = await _context.CareProviders.FindAsync(id);
            if (provider != null)
            {
                provider.Status = "LEFT";
                _context.CareProviders.Update(provider);
                await _context.SaveChangesAsync();
            }
        }
    }
}
