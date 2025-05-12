using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;

namespace UniSystem.Core.Services
{
    public interface IDepartmentService
    {
        Task<Department> FindDepartmentWithName(string DepName);
        Task<StudentDepartment> FindDepartmentWithStudentNo(string No);
        Task<List<StudentDepartment>> TakeStudentListByDepartment(int departmentId);
        Task<Department> FindDepartmentId(int id);
        Task<Response<List<DepartmentListDto>>> GetAllDepartment();




    }
}
