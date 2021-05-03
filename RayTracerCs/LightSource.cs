using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RayTracerCs
{
    public class LightSource : TracerObject
    {
        public LightSource(
            Vector3 origin,
            Quaternion rotation,
            List<Triangle> triangles,
            float totalFlux,
            SpectralColor spectralColor
        ) : base(origin, rotation, triangles)
        {
            TotalFlux = totalFlux;
            Spectral = spectralColor.Normalize();
        }

        public float TotalFlux { get; }
        public SpectralColor Spectral { get; }

        public SpectralColor Luminance(float sqaredDistance)
        {
            return new SpectralColor(
                Spectral.Values.Select(val => val * TotalFlux / (sqaredDistance)).ToArray()
            );
        }
    }
}