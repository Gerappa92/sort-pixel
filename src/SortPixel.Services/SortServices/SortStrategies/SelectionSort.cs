using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.SortServices
{
    public class SelectionSort : SortStrategyAbstract
    {
        public override Task<byte[]> Sort(Stream stream)
        {
            var task = new Task<byte[]>(() =>
            {
                byte[] result;

                Bitmap bitmap = new Bitmap(stream);
                var colorsArray = FlatteningBitmap(bitmap);

                for (int i = 0; i < colorsArray.Length; i++)
                {
                    float min = SumRGB(colorsArray[i]);
                    int minIndex = i;
                    for (int j = i +1 ; j < colorsArray.Length; j++)
                    {
                        var sumRGB = SumRGB(colorsArray[j]);
                        if (sumRGB <= min)
                        {
                            min = sumRGB;
                            minIndex = j;
                        }
                    }
                    var helper = colorsArray[i];
                    colorsArray[i] = colorsArray[minIndex];
                    colorsArray[minIndex] = helper;
                }

                UnflatteningColorsArray(bitmap, colorsArray);

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
    }
}
