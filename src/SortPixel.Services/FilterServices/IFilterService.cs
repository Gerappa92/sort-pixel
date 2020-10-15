﻿using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services
{
    public interface IFilterService
    {
        Task<byte[]> Filter(Stream stream);
    }
}
