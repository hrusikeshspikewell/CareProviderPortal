using CareProviderPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CareProviderPortal.Repository
{
    public class CareProviderRepository : ICareProviderRepository
    {
        private readonly CareProviderPortalContext _context;

        public CareProviderRepository(CareProviderPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CareProvider>> GetAll()
        {
            return await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();
        }

        public async Task<CareProvider> GetById(int id)
        {
            return await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

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

        public async Task<IEnumerable<CareProvider>> GetProvidersByDepartment(int departmentId)
        {
            return await _context.CareProviders
                .Where(p => p.DepartmentId == departmentId)
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();
        }

        public async Task<IEnumerable<CareProvider>> GetProvidersByExperience(int years)
        {
            var providers = await _context.CareProviders
                .Include(p => p.Experiences)
                .Include(p => p.Achievements)
                .ToListAsync();

            return providers.Where(p => CalculateTotalExperience(p.Experiences) >= years);
        }

        
        private int CalculateTotalExperience(ICollection<Experience> experiences)
        {
            double totalDays = experiences.Sum(exp =>
            {
                var start = exp.StartDate;
                var end = exp.EndDate.HasValue ? exp.EndDate.Value : DateTime.Now;
                return (end - start).TotalDays;
            });
            return (int)Math.Floor(totalDays / 365.25);
        }
    }
}
