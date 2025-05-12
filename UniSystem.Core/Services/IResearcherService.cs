using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models.ResearcherModels;

namespace UniSystem.Core.Services
{
    public interface IResearcherService
    {
        Task<Response<ResearcherInfoMainDto>> GetInformationForResarcher(string Email);
        Task<Response<ResearcherMetric>> GetInformationResarcherMetric(string Email);
        Task<Response<ResearcherEducationInformation>> GetEduInformation(string Email);
        Task<Response<List<ResearchArea>>> GetResearchAres(string Email);
        Task<Response<List<ResearcherPublicationDto>>> GetPublication(string Email);
        Task<Response<List<ResearcherExp>>> GetExp(string Email);
        Task<Response<ResearcherContact>> GetContact(string Email);
        Task<List<ResearcherSearchDto>> GetSuggestionsAsync(string query);






    }
}
