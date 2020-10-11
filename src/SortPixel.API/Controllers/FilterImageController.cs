using Microsoft.AspNetCore.Mvc;
using SortPixel.Services.FilterServices;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SortPixel.API.Controllers
{
    public class FilterImageController : BaseController
    {
        [HttpPost("rotate-image")]
        public async Task<IActionResult> Sort()
        {
            var file = HttpContext.Request.Form?.Files?.FirstOrDefault();
            if (file == null)
                return BadRequest();
            if (!file.ContentType.Contains("image"))
                return BadRequest("invalid content type");

            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var sortService = new FilterImageService();
                result = await sortService.RotateImageColor(ms);
            }
            return File(result, file.ContentType);
        }
    }
}
