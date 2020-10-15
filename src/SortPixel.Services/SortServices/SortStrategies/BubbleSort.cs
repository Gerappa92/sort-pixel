using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SortPixel.Services.SortServices.SortStrategies
{
    public class BubbleSort : SortStrategyAbstract
    {
        public override Task<byte[]> Sort(Stream stream)
        {
            var task = new Task<byte[]>(() =>
            {
                Bitmap bitmap = new Bitmap(stream);
                var colorsArray = FlatteningBitmap(bitmap);
                bool sort = true;
                while (sort)
                {
                    sort = false;
                    for (int i = 1; i < colorsArray.Length; i++)
                    {
                        Color left = colorsArray[i - 1];
                        Color right = colorsArray[i];
                        if(SumRGB(left) > SumRGB(right))
                        {
                            colorsArray[i - 1] = right;
                            colorsArray[i] = left;
                            sort = true;
                        }
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
