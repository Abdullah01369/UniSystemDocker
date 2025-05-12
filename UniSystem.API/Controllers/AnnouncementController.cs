using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Services;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]")]

    [ApiController]

    public class AnnouncementController : CustomBaseController
    {
        private readonly IServiceGeneric<Announcement, AnnouncementDto> _service;
        public AnnouncementController(IServiceGeneric<Announcement, AnnouncementDto> services)
        {
            _service = services;

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement(AnnouncementDto announcementDto)
        {
            announcementDto.AddingDate = DateTime.Now;
            var result = await _service.AddAsync(announcementDto);

            return ActionResultInstance(result);
        }

        // rolere yayınklarken bak
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAnnouncement()
        {
            return ActionResultInstance(await _service.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateAnnouncement(AnnouncementDto announcementDto)
        {
            return ActionResultInstance(await _service.Update(announcementDto, announcementDto.Id));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            return ActionResultInstance(await _service.Remove(id));
        }

    }
}
