using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.FilterServices
{
    public class FilterImageService : IFilterService
    {
        public Task<byte[]> RotateImageColor(Stream stream)
        {
            var task = new Task<byte[]>(() =>
            {
                var bitmap = new Bitmap(stream);
                var matrix = new int[bitmap.Width, bitmap.Height];
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
                byte[] result;
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, bitmap.RawFormat);
                    result = ms.ToArray();
                }
                return result;
            });
            task.Start();
            return task;
        }

        private int RotateColor(int component) => (component - 255) * -1;
    }
}
