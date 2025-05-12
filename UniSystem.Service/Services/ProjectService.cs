using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services
{

    public class ProjectService : IProjectService
    {

        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDepartmentService _departmentService;

        public ProjectService(IUserService userService, AppDbContext _appDbContext, IMapper mapper, UserManager<AppUser> userManager, IDepartmentService departmentService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = _appDbContext;
            _userService = userService;
            _departmentService = departmentService;
        }


        public async Task<Response<FileCountDto>> DocumentCount(int Id)
        {
            var val = await _context.ProjectFiles.Where(p => p.ProjectId == Id).CountAsync();

            if (val == null)
            {
                return Response<FileCountDto>.Fail("Proje bulunamadı", 400, true);
            }
            FileCountDto dto = new FileCountDto() { Count = val };
            return Response<FileCountDto>.Success(dto, 200);

        }


        public async Task<Response<StudentDto>> AddStudentToProject(AddStudentToProjectRequest request)
        {
            if (request == null)
            {
                return Response<StudentDto>.Fail("Uygun formatta gelmeyen veriler var !", 400, true);
            }

            var val = await GetProjectById(int.Parse(request.ProjectId));
            if (val == null)
            {
                return Response<StudentDto>.Fail("Proje bulunamadı!", 400, true);

            }


            var student = await _userService.FindUserByStudentNo(request.StudentNo);

            if (student == null)
            {
                return Response<StudentDto>.Fail("Öğrenci bulunamadı!", 400, true);

            }

            var studentIsHave = await StudentIsInProject(request.StudentNo, int.Parse(request.ProjectId));

            if (studentIsHave == true)
            {
                return Response<StudentDto>.Fail("Öğrenci zaten projeye dahil!", 400, true);

            }



            ProjectStudent projectStudent = new ProjectStudent
            {
                AddingDate = DateTime.Now,
                AppUser = student,

                IsActive = true,
                ProjectId = val.Data.Id

            };

            var dep = await _departmentService.FindDepartmentWithStudentNo(request.StudentNo);
            StudentDto dto = new StudentDto()
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Department = dep.Department.Name
            };

            _context.ProjectStudents.Add(projectStudent);

            return Response<StudentDto>.Success(dto, 200);

        }


        public async Task<Response<NoDataDto>> CreateProject(CreateProjectDto request)
        {
            var val = await _userManager.FindByEmailAsync(request.AcademicianMail);
            if (val == null)
            {

                return Response<NoDataDto>.Fail("Akademisyen Bulunamadı", 400, true);

            }

            Project project = new Project
            {
                Statu = "true",
                AppUser = val,
                CrearedDate = DateTime.Now,
                IsActive = true,
                IsPublish = false,
                IsComplate = false,
                Name = request.Name,
                Subject = request.Declaration,


            };
            await _context.Projects.AddAsync(project);
            return Response<NoDataDto>.Success(StatusCodes.Status201Created);
        }

        public async Task<Response<List<StudentListForProjectDto>>> GetStudentListByProjectId(int Id)
        {
            var val = await _context.ProjectStudents.Where(p => p.ProjectId == Id && p.IsActive == true).Include(x => x.AppUser).ToListAsync();


            if (val == null)
            {
                return Response<List<StudentListForProjectDto>>.Fail("bir problemle karsilasıldı", 400, true);
            }

            var mappedMessages = _mapper.Map<List<StudentListForProjectDto>>(val);
            return Response<List<StudentListForProjectDto>>.Success(mappedMessages, 200);

        }

        public async Task<Response<ProjectDto>> GetProjectById(int Id)
        {
            var val = await _context.Projects.Where(p => p.Id == Id && p.IsActive == true).Include(x => x.AppUser).FirstOrDefaultAsync();

            if (val == null)
            {
                return Response<ProjectDto>.Fail("Proje bulunamadı", 400, true);
            }
            var mappedMessages = _mapper.Map<ProjectDto>(val);
            return Response<ProjectDto>.Success(mappedMessages, 200);

        }

        public async Task<Response<List<ProjectDto>>> GetProjectListByAcademician(string Email)
        {
            var val = await _context.Projects.Where(p => p.AppUser.Email == Email && p.IsActive == true).Include(x => x.AppUser).AsNoTracking().ToListAsync();

            if (val == null)
            {
                return Response<List<ProjectDto>>.Fail("Proje bulunamadı", 400, true);
            }
            var mappedMessages = _mapper.Map<List<ProjectDto>>(val);
            return Response<List<ProjectDto>>.Success(mappedMessages, 200);

        }


        public async Task<bool> StudentIsInProject(string No, int ProjectId)
        {
            var val = await _context.ProjectStudents.Where(x => x.ProjectId == ProjectId && x.AppUser.No == No).Include(x => x.AppUser).AsNoTracking().FirstOrDefaultAsync();

            if (val != null)
            {
                return true;
            }
            return false;

        }


    }

}
