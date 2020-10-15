using Microsoft.AspNetCore.Mvc;
using SortPixel.Services.SortServices;
using SortPixel.Services.SortServices.SortStrategies;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SortPixel.API.Controllers
{
    public class SortPixelsController : BaseController
    {
        [HttpPost("selection")]
        public async Task<IActionResult> Selection() => await Sort(new SelectionSort());

        [HttpPost("bubble")]
        public async Task<IActionResult> Bubble() => await Sort(new BubbleSort());

        [HttpPost("insertion")]
        public async Task<IActionResult> Insertion() => await Sort(new InsertionSort());

        private async Task<IActionResult> Sort(ISortStrategy sortStrategy)
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
                var sortService = new SortPixelsService(sortStrategy);
                result = await sortService.SortPixels(ms);
            }
            return File(result, file.ContentType);
        }
    }
}
