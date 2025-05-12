using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;
using UniSystem.Core.Services;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResearcherController : CustomBaseController
    {
        private readonly IResearcherService _researcherService;
        public ResearcherController(IResearcherService researcherService)
        {
            _researcherService = researcherService;

        }
        #region method
        [HttpGet]
        public async Task<IActionResult> GetInformationForResarcher(string Email)
        {

            var val = await _researcherService.GetInformationForResarcher(Email);
            return ActionResultInstance(val);
        }
        [HttpGet]
        public async Task<IActionResult> GetInformationResarcherMetrics(string Email)
        {
            var val = await _researcherService.GetInformationResarcherMetric(Email);
            return ActionResultInstance(val);
        }
        [HttpGet]
        public async Task<IActionResult> GetInformationEdu(string Email)
        {
            var val = await _researcherService.GetEduInformation(Email);
            return ActionResultInstance(val);
        }
        [HttpGet]
        public async Task<IActionResult> GetResearhAreas(string Email)
        {
            var val = await _researcherService.GetResearchAres(Email);
            return ActionResultInstance(val);
        }
        [HttpGet]
        public async Task<IActionResult> GetPublications(string Email)
        {
            var val = await _researcherService.GetPublication(Email);
            return ActionResultInstance(val);
        }
        [HttpGet]
        public async Task<IActionResult> GetExperience(string Email)
        {
            var val = await _researcherService.GetExp(Email);
            return ActionResultInstance(val);
        }
        [HttpGet]
        public async Task<IActionResult> GetContactInfo(string Email)
        {
            var val = await _researcherService.GetContact(Email);
            return ActionResultInstance(val);
        }

        #endregion



        [HttpGet]
        public async Task<IActionResult> GetSuggestions([FromQuery] string query)
        {

            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
                return Ok(new List<object>());

            var suggestions = await _researcherService.GetSuggestionsAsync(query);

            return Ok(suggestions);
        }



    }
}
