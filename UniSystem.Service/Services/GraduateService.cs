using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services
{
    public class GraduateService : IGraduateService
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDepartmentService _departmentService;

        public GraduateService(AppDbContext _appDbContext, IMapper mapper)
        {
            _mapper = mapper;

            _context = _appDbContext;

        }
        public async Task<Response<GradutedDto>> GetGradutedInfo(string Email)
        {
            var val = await _context.GradutedStatusStudents.Where(x => x.AppUser.Email == Email).FirstOrDefaultAsync();
            var mappedval = _mapper.Map<GradutedDto>(val);

            return Response<GradutedDto>.Success(mappedval, 200);

        }
    }
}
