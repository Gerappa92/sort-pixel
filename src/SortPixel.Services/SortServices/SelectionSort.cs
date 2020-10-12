using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SortPixel.Services.SortServices
{
    public class SelectionSort : ISortStrategy
    {
        public Task<byte[]> Sort(Stream stream)
        {
            var task = new Task<byte[]>(() =>
            {
                byte[] result;

                Bitmap bitmap = new Bitmap(stream);
                var flatImageArray = new Color[bitmap.Height * bitmap.Width];
                var helperIndex = 0;
                for (int i = 0; i < bitmap.Height; i++)
                {
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        flatImageArray[helperIndex] = bitmap.GetPixel(j, i);
                        helperIndex++;
                    }
                }

                for (int i = 0; i < flatImageArray.Length; i++)
                {
                    float min = SumRGB(flatImageArray[i]);
                    int minIndex = i;
                    for (int j = i; j < flatImageArray.Length; j++)
                    {
                        var sumRGB = SumRGB(flatImageArray[j]);
                        if (sumRGB <= min)
                        {
                            min = sumRGB;
                            minIndex = j;
                        }
                    }
                    var helper = flatImageArray[i];
                    flatImageArray[i] = flatImageArray[minIndex];
                    flatImageArray[minIndex] = helper;
                }

                helperIndex = 0;
                for (int i = 0; i < bitmap.Height; i++)
                {
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        bitmap.SetPixel(j, i, flatImageArray[helperIndex]);
                        helperIndex++;
                    }
                }

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

        private int SumRGB(Color color) => color.R + color.G + color.B;
    }
}
