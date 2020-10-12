using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SortPixel.Services.SortServices
{
    public class SortPixelsService
    {
        private ISortStrategy _sortStrategy;
        public SortPixelsService(ISortStrategy sortStrategy)
        {
            _sortStrategy = sortStrategy;
        }

        public async Task<byte[]> SortPixels(Stream stream)
        {
            return await _sortStrategy.Sort(stream);
        }
    }
}
