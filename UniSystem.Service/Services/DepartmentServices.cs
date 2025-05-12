using AutoMapper;

using Microsoft.EntityFrameworkCore;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Repositories;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services
{
    public class DepartmentServices : IDepartmentService
    {
        private readonly IGenericRepository<Department> _DepartmentServices;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public DepartmentServices(IGenericRepository<Department> DepartmentServices, AppDbContext appDbContext, IMapper mapper)
        {

            _mapper = mapper;
            _appDbContext = appDbContext;
            _DepartmentServices = DepartmentServices;

        }

        public async Task<Department> FindDepartmentWithName(string DepName)
        {
            var val = await _DepartmentServices.Where(x => x.Name == DepName).Include(x => x.Faculty).FirstOrDefaultAsync();

            return val;
        }

        public async Task<Department> FindDepartmentId(int id)
        {
            var val = await _DepartmentServices.Where(x => x.Id == id).Include(x => x.Faculty).FirstOrDefaultAsync();

            return val;
        }



        public async Task<List<StudentDepartment>> TakeStudentListByDepartment(int departmentId)
        {
            var val2 = await _appDbContext.StudentDepartments.ToListAsync();
            var val = await _appDbContext.StudentDepartments.Where(x => x.DepartmentId == departmentId).Include(x => x.AppUser).Include(x => x.Department).AsNoTracking().ToListAsync();

            return val;
        }

        public async Task<StudentDepartment> FindDepartmentWithStudentNo(string No)
        {
            var val = await _appDbContext.StudentDepartments.Where(x => x.AppUser.No == No).Include(x => x.AppUser).Include(x => x.Department).AsNoTracking().FirstOrDefaultAsync();

            return val;
        }

        public async Task<Response<List<DepartmentListDto>>> GetAllDepartment()
        {
            var val = await _appDbContext.Departments.ToListAsync();
            var mappedval = _mapper.Map<List<DepartmentListDto>>(val);
            var response = Response<List<DepartmentListDto>>.Success(mappedval, 200);
            return response;
        }

    }
}
