using CareProviderPortal.Models;

namespace CareProviderPortal.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(int id);
        Task<Department> Add(Department department);
        Task Update(Department department);
        Task Delete(int id);
        Task<Department> GetDepartmentWithCareProviders(int departmentId);
    }
}
