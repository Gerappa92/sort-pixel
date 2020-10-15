using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.FilterServices.FilterStrategies
{
    public class RotateColorsStrategy : FilterStrategyAbstract
    {
        public override Task<byte[]> Filter(Stream stream)
        {
            var task = new Task<byte[]>(() =>
            {
                var bitmap = new Bitmap(stream);
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        var pixel = bitmap.GetPixel(i, j);
                        int r, g, b;
                        r = RotateColor(pixel.R);
                        g = RotateColor(pixel.G);
                        b = RotateColor(pixel.B);
                        Color color = Color.FromArgb(r, g, b);
                        bitmap.SetPixel(i, j, color);
                    }
                }
                return BitmapToByteArray(bitmap);
            });
            task.Start();
            return task;
        }

        private int RotateColor(int component) => (component - 255) * -1;
    }
}
