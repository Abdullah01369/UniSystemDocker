using Microsoft.AspNetCore.Mvc;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : CustomBaseController
    {

        //[HttpGet("download/{id}")]
        //public async Task<IActionResult> DownloadDocument(Guid id)
        //{
        //    var document = await _documentRepository.FindAsync(d => d.Id == id);

        //    if (document == null || document.ExpireAt <= DateTime.Now)
        //    {
        //        return NotFound("Belge bulunamadı veya süresi dolmuş.");
        //    }

        //    var fileStream = new FileStream(document.FilePath, FileMode.Open, FileAccess.Read);
        //    return File(fileStream, "application/pdf", Path.GetFileName(document.FilePath));
        //}

    }
}
