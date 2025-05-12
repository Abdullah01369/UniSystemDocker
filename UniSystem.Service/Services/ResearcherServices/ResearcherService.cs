using AutoMapper;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Models.ResearcherModels;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services.ResearcherServices
{
    public class ResearcherService : IResearcherService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ResearcherService(AppDbContext _appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _context = _appDbContext;

        }

        #region methods
        public async Task<Response<ResearcherInfoMainDto>> GetInformationForResarcher(string Email)
        {
            var val = await _context.Researchers.Where(p => p.AppUser.Email == Email).Include(x => x.AppUser).FirstOrDefaultAsync();

            if (val == null)
            {
                return Response<ResearcherInfoMainDto>.Fail("Kişi bulunamadı", 400, true);
            }
            var mappedMessages = _mapper.Map<ResearcherInfoMainDto>(val);
            mappedMessages.Name = val.AppUser.Name;
            mappedMessages.Surname = val.AppUser.Surname;

            return Response<ResearcherInfoMainDto>.Success(mappedMessages, 200);

        }
        public async Task<Response<ResearcherMetric>> GetInformationResarcherMetric(string Email)
        {
            var val = await _context.ResearcherMetrics.Where(p => p.Researcher.AppUser.Email == Email).FirstOrDefaultAsync();

            if (val == null)
            {
                return Response<ResearcherMetric>.Fail("Kişi bulunamadı", 400, true);
            }


            return Response<ResearcherMetric>.Success(val, 200);

        }
        public async Task<Response<ResearcherEducationInformation>> GetEduInformation(string Email)
        {
            var val = await _context.ResearcherEducationInformations.Where(p => p.Researcher.AppUser.Email == Email).FirstOrDefaultAsync();

            if (val == null)
            {
                return Response<ResearcherEducationInformation>.Fail("Kişi bulunamadı", 400, true);
            }


            return Response<ResearcherEducationInformation>.Success(val, 200);

        }
        public async Task<Response<List<ResearchArea>>> GetResearchAres(string Email)
        {
            var val = await _context.ResearchAreas.Where(p => p.Researcher.AppUser.Email == Email).ToListAsync();
            return Response<List<ResearchArea>>.Success(val, 200);

        }
        public async Task<Response<List<ResearcherExp>>> GetExp(string Email)
        {
            var val = await _context.ResearcherExps.Where(p => p.Researcher.AppUser.Email == Email).ToListAsync();
            var mappedMessages = _mapper.Map<List<ResearcherExp>>(val);
            return Response<List<ResearcherExp>>.Success(val, 200);

        }
        public async Task<Response<List<ResearcherPublicationDto>>> GetPublication(string Email)
        {
            var val = await _context.ResearcherPublications.Where(p => p.Researcher.AppUser.Email == Email).Include(x => x.PublicationMembers).ToListAsync();
            var mappedval = _mapper.Map<List<ResearcherPublicationDto>>(val);

            return Response<List<ResearcherPublicationDto>>.Success(mappedval, 200);

        }
        public async Task<Response<ResearcherContact>> GetContact(string Email)
        {
            var val = await _context.ResearcherContacts.Where(x => x.Researcher.AppUser.Email == Email).FirstOrDefaultAsync();
            return Response<ResearcherContact>.Success(val, 200);

        }
        #endregion



        public async Task<List<ResearcherSearchDto>> GetSuggestionsAsync(string query)
        {

            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
                return new List<ResearcherSearchDto>();


            var suggestions = await _context.Researchers
        .Join(_context.Users,
              r => r.AppUserId,
              u => u.Id,
              (r, u) => new { Researcher = r, User = u })
        .Where(x => EF.Functions.Like(x.User.Name, $"%{query}%") ||
                    EF.Functions.Like(x.User.Surname, $"%{query}%") ||
                    EF.Functions.Like(x.User.Email, $"%{query}%"))
        .Select(x => new ResearcherSearchDto
        {
            Id = x.Researcher.Id,
            Surname = x.User.Surname,
            Name = x.User.Name,
             Email=x.User.Email
            
        })
        .ToListAsync();



            return suggestions;
        }

        private string DetermineMatchType(AppUser user, string query)
        {
            if (user.Name.Equals(query, StringComparison.OrdinalIgnoreCase))
                return "Exact";
            if (user.Name.StartsWith(query, StringComparison.OrdinalIgnoreCase))
                return "StartsWith";
            if (user.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                return "Contains";
            return "Partial";
        }

        private int GetMatchPriority(string matchType)
        {
            return matchType switch
            {
                "Exact" => 1,
                "StartsWith" => 2,
                "Contains" => 3,
                "Partial" => 4,
                _ => 5
            };
        }
    }
}
