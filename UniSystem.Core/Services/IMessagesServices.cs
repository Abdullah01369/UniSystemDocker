using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;

namespace UniSystem.Core.Services
{
    public interface IMessagesServices
    {
        public Task<bool> SendMail(SendMailRequestDto request);
        public Task<Response<ICollection<InBoxListDto>>> InBoxList(string EMail);
        public Task<Response<InBoxListDto>> GetInBox(int Id);
        public Task<Response<FileDto>> DownloadFile(int Id);
        public Task<Response<ICollection<InBoxListDto>>> DraftList(string EMail);
        public Task<Response<NoDataDto>> DelDraft(int Id);
        public Task<Response<ICollection<InBoxListDto>>> OutBoxList(string EMail);
        public Task<string> SendMultiMail(SendMultiMailModel model, string year, string period);







    }
}
