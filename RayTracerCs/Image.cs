using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RayTracerCs
{
    public class Image
    {
        Image(uint w, uint h)
        {
            var pixels = new SpectralColor[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    pixels[i, j] = SpectralColor.Zero();
                }
            }

            Pixels = pixels;
            HitsCount = new uint[w, h];
        }

        public SpectralColor[,] Pixels { get; }
        public uint[,] HitsCount { get; }

        public int Width => Pixels.GetLength(0);
        public int Height => Pixels.GetLength(1);

        public void AccumulatePixel(uint x, uint y, SpectralColor luminance)
        {
            lock (this)
            {
                var pixel = Pixels[x, y];
                var hits = HitsCount[x, y] + 1;
                var newValues = pixel.Values.Zip(luminance.Values, (o, n) => o + (n - o) / (float) hits).ToArray();
                Pixels[x, y] = new SpectralColor(newValues);
                HitsCount[x, y] = hits;
            }
        }
    }
}