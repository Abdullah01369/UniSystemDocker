using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDepartmentService _departmentService;

        public CourseServices(IUserService userService, AppDbContext _appDbContext, IMapper mapper, UserManager<AppUser> userManager, IDepartmentService departmentService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = _appDbContext;
            _userService = userService;
            _departmentService = departmentService;
        }


        public async Task<Response<PaggingDto>> AllCourseList(int page, int pageSize, string search)
        {
            try
            {
                List<Lesson> lessonlist = new List<Lesson>();

                lessonlist = await _context.Lessons.Include(x => x.Department).ToListAsync();

                if (!string.IsNullOrEmpty(search))
                {
                    lessonlist = await _context.Lessons.Where(l =>
                          l.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                          l.Code.Contains(search, StringComparison.OrdinalIgnoreCase)).ToListAsync();

                }

                var paginatedData = lessonlist
              .Skip((page - 1) * pageSize)
              .Take(pageSize)
              .ToList();


                var mappedval = _mapper.Map<List<CourseDto>>(paginatedData);

                PaggingDto pagination = new PaggingDto();
                pagination.Page = lessonlist.Count;
                pagination.CourseList = mappedval;
                return Response<PaggingDto>.Success(pagination, 200);
            }
            catch (Exception ex)
            {

                return Response<PaggingDto>.Fail("bir şeyler yanlış gitti", 500, true);

            }

        }


        public async Task<Response<PaggingDto>> SearchCourse(string search)
        {
            try
            {

                //var lessonlist = await _context.Lessons.Where(l =>
                //        l.Name.Contains(search) ||
                //        l.Code.Contains(search)).ToListAsync();

                var lessonlist = await _context.Lessons.Include(x => x.Department)
      .Where(l => EF.Functions.Like(l.Name, $"%{search}%") ||
                  EF.Functions.Like(l.Code, $"%{search}%"))
      .ToListAsync();


                var mappedval = _mapper.Map<List<CourseDto>>(lessonlist);

                PaggingDto pagination = new PaggingDto();
                pagination.Page = lessonlist.Count;
                pagination.CourseList = mappedval;
                return Response<PaggingDto>.Success(pagination, 200);
            }
            catch (Exception ex)
            {

                return Response<PaggingDto>.Fail("bir şeyler yanlış gitti", 500, true);

            }

        }

        public async Task<Response<AddCourseDto>> AddCourse(AddCourseDto model)
        {
            var department = await _departmentService.FindDepartmentId(model.DepartmentId);
            if (department == null)
            {
                return Response<AddCourseDto>.Fail("Department id is not valid", 404, true);

            }
            var mappedval = _mapper.Map<Lesson>(model);
            var val = await _context.Lessons.AddAsync(mappedval);
            await _context.SaveChangesAsync();

            return Response<AddCourseDto>.Success(model, 201);

        }

        public async Task<Response<CourseDto>> GetCourseById(int Id)
        {
            var val = await _context.Lessons.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (val == null)
            {

                return Response<CourseDto>.Fail("lesson  is not finded", 404, true);
            }
            var mappedval = _mapper.Map<CourseDto>(val);
            return Response<CourseDto>.Success(mappedval, 201);
        }

        public async Task<Response<EditCourseDto>> EditCourse(EditCourseDto model)
        {
            var lesson = await _context.Lessons.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            var mappedval = _mapper.Map<Lesson>(model);
            var val = _mapper.Map(model, lesson);
            await _context.SaveChangesAsync();

            return Response<EditCourseDto>.Success(model, 201);

        }

        public async Task<Response<CourseStatisticDto>> GetStatistic()
        {
            var lessonsum = await _context.Lessons.CountAsync();
            var lessonactive = await _context.Lessons.CountAsync(x => x.Status == true);
            var lessonpassive = await _context.Lessons.CountAsync(x => x.Status == false);

            CourseStatisticDto statistic = new CourseStatisticDto()
            {
                Active = lessonactive.ToString(),
                Passive = lessonpassive.ToString(),
                Sum = lessonsum.ToString()
            };


            return Response<CourseStatisticDto>.Success(statistic, 201);

        }

    }
}
