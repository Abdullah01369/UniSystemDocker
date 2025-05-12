using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;

namespace UniSystem.Core.Services
{
    public interface IProjectService
    {
        public Task<Response<ProjectDto>> GetProjectById(int Id);
        public Task<Response<List<ProjectDto>>> GetProjectListByAcademician(string Email);
        public Task<Response<NoDataDto>> CreateProject(CreateProjectDto request);
        public Task<Response<List<StudentListForProjectDto>>> GetStudentListByProjectId(int Id);
        public Task<Response<FileCountDto>> DocumentCount(int Id);
        Task<Response<StudentDto>> AddStudentToProject(AddStudentToProjectRequest request);
        Task<bool> StudentIsInProject(string No, int ProjectId);








    }
}
