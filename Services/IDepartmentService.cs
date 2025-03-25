using CareProviderPortal.dto;

namespace CareProviderPortal.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartments();
        Task<DepartmentDTO> GetDepartmentById(int id);
        Task<DepartmentDTO> AddDepartment(DepartmentCreateDTO departmentCreateDTO);
        Task UpdateDepartment(int id, DepartmentCreateDTO departmentCreateDTO);
        Task DeleteDepartment(int id);
    }

}
