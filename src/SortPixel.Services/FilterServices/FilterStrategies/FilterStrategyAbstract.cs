using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.FilterServices.FilterStrategies
{
    public abstract class FilterStrategyAbstract : IFilterStrategy
    {
        public abstract Task<byte[]> Filter(Stream stream);

        protected byte[] BitmapToByteArray(Bitmap bitmap)
        {
            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                result = ms.ToArray();
            }
            return result;
        }

    }
}
