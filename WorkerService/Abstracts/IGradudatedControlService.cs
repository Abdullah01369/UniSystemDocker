using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.ModelDto;

namespace WorkerService.Abstracts
{
    public interface IGradudatedControlService
    {
        Task<GradutedDto> StudentGradutedControl(string StudentNo);

    }
}
