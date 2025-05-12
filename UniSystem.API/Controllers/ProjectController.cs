using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;
using UniSystem.Core.UnitOfWork;

namespace UniSystem.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "academician")]
    [ApiController]
    public class ProjectController : CustomBaseController
    {
        private readonly IProjectService _service;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectController(IProjectService service, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _service = service;

        }
        [HttpPost]
        public async Task<IActionResult> DocumentCount(int id)
        {
            var val = await _service.DocumentCount(id);
            return ActionResultInstance(val);
        }
        [HttpPost]
        public async Task<IActionResult> GetStudentListByProject(int id)
        {
            var val = await _service.GetStudentListByProjectId(id);
            return ActionResultInstance(val);
        }


        [HttpPost]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var val = await _service.GetProjectById(id);
            return ActionResultInstance(val);
        }
        [HttpPost]
        public async Task<IActionResult> GetProjectListByAcademicianMail(string Email)
        {
            var val = await _service.GetProjectListByAcademician(Email);
            return ActionResultInstance(val);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto request)
        {
            var val = await _service.CreateProject(request);
            await _unitOfWork.CommmitAsync();
            return ActionResultInstance(val);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentToProject(AddStudentToProjectRequest request)
        {
            var val = await _service.AddStudentToProject(request);
            await _unitOfWork.CommmitAsync();
            return ActionResultInstance(val);
        }



    }
}
