using AutoMapper;
using CareProviderPortal.dto;
using CareProviderPortal.Models;
using CareProviderPortal.Repository;

namespace CareProviderPortal.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartments()
        {
            var departments = await _repository.GetAll();
            return _mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        public async Task<DepartmentDTO> GetDepartmentById(int id)
        {
            var department = await _repository.GetById(id);
            return _mapper.Map<DepartmentDTO>(department);
        }

        public async Task<DepartmentDTO> AddDepartment(DepartmentCreateDTO departmentCreateDTO)
        {
            var department = _mapper.Map<Department>(departmentCreateDTO);
            var createdDepartment = await _repository.Add(department);
            return _mapper.Map<DepartmentDTO>(createdDepartment);
        }

        public async Task UpdateDepartment(int id, DepartmentCreateDTO departmentCreateDTO)
        {
            
            var existingDepartment = await _repository.GetById(id);
            if (existingDepartment == null)
            {
                throw new Exception("Department not found.");
            }

            existingDepartment.Name = departmentCreateDTO.Name;
            existingDepartment.Description = departmentCreateDTO.Description;

            await _repository.Update(existingDepartment);
        }

        public async Task DeleteDepartment(int id)
        {
            await _repository.Delete(id);
        }
    }
}
