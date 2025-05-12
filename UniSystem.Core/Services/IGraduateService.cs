using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSystem.Core.DTOs;

namespace UniSystem.Core.Services
{
    public interface IGraduateService
    {
        Task<Response<GradutedDto>> GetGradutedInfo(string Email);

    }
}
