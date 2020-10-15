using Microsoft.AspNetCore.Mvc;
using SortPixel.Services.FilterServices;
using SortPixel.Services.FilterServices.FilterStrategies;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SortPixel.API.Controllers
{
    public class FilterImageController : BaseController
    {
        [HttpPost("rotate-colors")]
        public async Task<IActionResult> RotateColors() => await Filter(new RotateColorsStrategy());

        [HttpPost("left-neighbor")]
        public async Task<IActionResult> LeftNeighbor() => await Filter(new LeftNeighborStrategy());


        private async Task<IActionResult> Filter(IFilterStrategy filterStrategy)
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
                var sortService = new FilterService(filterStrategy);
                result = await sortService.Filter(ms);
            }
            return File(result, file.ContentType);
        }


    }
}
