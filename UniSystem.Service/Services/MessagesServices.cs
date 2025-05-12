using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Dtos;
using SharedLayer.RabbitMQ;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services
{
    public class MessagesServices : IMessagesServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly RabbitMqPublisherService _rabbitMqPublisherService;


        public MessagesServices(AppDbContext _appDbContext, IMapper mapper, RabbitMqPublisherService rabbitMqPublisherService)
        {
            _mapper = mapper;
            _context = _appDbContext;
            _rabbitMqPublisherService = rabbitMqPublisherService;
        }



        public async Task<string> SendMultiMail(SendMultiMailModel model, string year, string period)
        {
            var val = await _context.Exams.Where(x => x.AcademicianCanGiveLesson.AppUser.Email == model.AcademicianMail && x.AcademicianCanGiveLesson.LessonId == model.LessonId && x.AcademicianCanGiveLesson.AcademicYear.YearOfEducation == year && x.AcademicianCanGiveLesson.AcademicYear.Period == period).Include(x => x.AppUser).Select(x => x.AppUser.Email).ToListAsync();

            RabbitMqMailMessage msg = new RabbitMqMailMessage()
            {
                AcademicianMail = model.AcademicianMail,
                FileName = model.FileName,
                LessonId = model.LessonId,
                messagearea = model.messagearea,
                MessageFileTxt = model.MessageFileTxt,
                subject = model.subject,
                period = period,
                year = year

            };
            _rabbitMqPublisherService.Publish(msg);

            return "true";
        }
        public async Task<bool> SendMail(SendMailRequestDto request)
        {
            if (request != null)
            {

                Message message = new Message()
                {
                    FileName = request.FileName,
                    Content = request.Content,
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    IsDraft = request.IsDraft,
                    IsSended = true,
                    MessageFileTxt = request.MessageFileTxt,
                    ReceiverMail = request.ReceiverMail,
                    SenderMail = request.SenderMail,
                    Title = request.Title


                };

                if (message.IsDraft == true)
                {
                    message.IsSended = false;
                    message.IsActive = false;
                }
                await _context.Messages.AddAsync(message);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;


            }
        }


        public async Task<Response<ICollection<InBoxListDto>>> InBoxList(string EMail)
        {
            //var val = await _context.Messages.Where(x => x.IsSended == true && x.IsActive == true && x.ReceiverMail == EMail && x.IsDraft != true).OrderByDescending(x => x.Date).ToListAsync();

            var val = _context.Messages
    .AsNoTracking()
    .Where(x => x.IsSended && x.IsActive && !x.IsDraft && !x.IsDeleted && x.ReceiverMail == EMail)
    .OrderByDescending(x => x.Date)
    .ToList();

            var mappedMessages = _mapper.Map<List<InBoxListDto>>(val);

            return Response<ICollection<InBoxListDto>>.Success(mappedMessages, 200);
        }

        public async Task<Response<InBoxListDto>> GetInBox(int Id)
        {
            var val = await _context.Messages.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (val == null)
            {

                return Response<InBoxListDto>.Fail("Mail bulunamadı", 400, true);

            }
            var mappedMessages = _mapper.Map<InBoxListDto>(val);

            return Response<InBoxListDto>.Success(mappedMessages, 200);
        }

        public async Task<Response<FileDto>> DownloadFile(int Id)
        {
            var val = await _context.Messages.Where(x => x.Id == Id).FirstOrDefaultAsync();

            FileDto fileDto = new FileDto()
            {
                FileName = val.FileName,
                MessageFileTxt = val.MessageFileTxt,
                Id = Id
            };
            if (val == null)
            {

                return Response<FileDto>.Fail("Bulunamadı", 400, true);

            }


            return Response<FileDto>.Success(fileDto, 200);
        }

        public async Task<Response<ICollection<InBoxListDto>>> DraftList(string EMail)
        {
            var val = _context.Messages.Where(x => x.IsSended == false && x.IsActive == false && x.SenderMail == EMail && x.IsDraft == true).OrderByDescending(x => x.Date).ToList();
            var mappedMessages = _mapper.Map<List<InBoxListDto>>(val);

            return Response<ICollection<InBoxListDto>>.Success(mappedMessages, 200);
        }


        public async Task<Response<NoDataDto>> DelDraft(int Id)
        {


            try
            {
                var val = _context.Database.ExecuteSqlInterpolated($"EXEC DelDraft {Id}");


                return Response<NoDataDto>.Success(200);
            }
            catch (Exception)
            {
                return Response<NoDataDto>.Fail("Silinemedi", 400, true);
                throw;
            }





        }

        public async Task<Response<ICollection<InBoxListDto>>> OutBoxList(string EMail)
        {
            var val = _context.Messages
             .AsNoTracking()
                .Where(x => x.IsSended && x.IsActive && !x.IsDraft && x.SenderMail == EMail)
                     .OrderByDescending(x => x.Date)
                        .ToList();

            var mappedMessages = _mapper.Map<List<InBoxListDto>>(val);

            return Response<ICollection<InBoxListDto>>.Success(mappedMessages, 200);
        }
    }
}
