using Microsoft.EntityFrameworkCore;
using CareProviderPortal.Models;

namespace CareProviderPortal.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CareProviderPortalContext _context;

        public DepartmentRepository(CareProviderPortalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> Add(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task Update(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Department> GetDepartmentWithCareProviders(int departmentId)
        {
            return await _context.Departments
                .Include(d => d.CareProviders)
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }
    }
}
