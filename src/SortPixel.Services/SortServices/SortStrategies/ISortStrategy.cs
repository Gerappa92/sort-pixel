using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.SortServices
{
    public interface ISortStrategy
    {
        Task<byte[]> Sort(Stream stream);
    }
}
