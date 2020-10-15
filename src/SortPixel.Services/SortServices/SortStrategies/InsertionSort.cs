using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.SortServices.SortStrategies
{
    public class InsertionSort : SortStrategyAbstract
    {
        public override Task<byte[]> Sort(Stream stream)
        {
            var task = new Task<byte[]>(() =>
            {
                Bitmap bitmap = new Bitmap(stream);
                var colorsArray = FlatteningBitmap(bitmap);

                for (int i = 1; i < colorsArray.Length; i++)
                {
                    Color right = colorsArray[i];
                    int rightRGB = SumRGB(right);
                    for (int j = i - 1; j >= 0; j--)
                    {
                        Color left = colorsArray[j];
                        int leftRGB = SumRGB(left);
                        if (leftRGB > rightRGB)
                        {
                            colorsArray[j + 1] = left;
                            colorsArray[j] = right;
                        }
                        else
                            break;
                    }
                }

                UnflatteningColorsArray(bitmap, colorsArray);
                return BitmapToByteArray(bitmap);
            });
            task.Start();
            return task;
        }
    }
}
