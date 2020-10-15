using SortPixel.Services.FilterServices.FilterStrategies;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.FilterServices
{
    public class FilterService : IFilterService
    {
        private readonly IFilterStrategy _filterStrategy;

        public FilterService(IFilterStrategy filterStrategy)
        {
            _filterStrategy = filterStrategy;
        }

        public async Task<byte[]> Filter(Stream stream)
        {
            return await _filterStrategy.Filter(stream);
        }
    }
}
