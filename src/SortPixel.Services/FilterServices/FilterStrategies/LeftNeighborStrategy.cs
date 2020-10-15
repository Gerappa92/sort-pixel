using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.FilterServices.FilterStrategies
{
    public class LeftNeighborStrategy : FilterStrategyAbstract
    {
        public override Task<byte[]> Filter(Stream stream)
        {
            var task = new Task<byte[]>(() =>
            {

                Bitmap bitmap = new Bitmap(stream);
                for (int i = 0; i < bitmap.Height; i++)
                {
                    for (int j = 1; j < bitmap.Width; j++)
                    {
                        var leftNeighbor = bitmap.GetPixel(j - 1, i);
                        bitmap.SetPixel(j, i, leftNeighbor);
                    }
                }

                return BitmapToByteArray(bitmap);
            });
            task.Start();
            return task;
        }
    }
}
