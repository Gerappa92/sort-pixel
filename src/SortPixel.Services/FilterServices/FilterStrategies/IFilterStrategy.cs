using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.FilterServices.FilterStrategies
{
    public interface IFilterStrategy
    {
        Task<byte[]> Filter(Stream stream);
    }
}
