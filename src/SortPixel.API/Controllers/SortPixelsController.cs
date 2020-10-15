using Microsoft.AspNetCore.Mvc;
using SortPixel.Services.SortServices;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SortPixel.API.Controllers
{
    public class SortPixelsController : BaseController
    {
        [HttpPost("selection-sort")]
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
                var sortService = new SortPixelsService(new SelectionSort());
                result = await sortService.SortPixels(ms);
            }
            return File(result, file.ContentType);
        }
    }
}
