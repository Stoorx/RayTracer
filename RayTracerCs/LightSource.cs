using System.Collections.Generic;
using System.Numerics;

namespace RayTracerCs
{
    public class LightSource : TracerObject
    {
        public LightSource(
            Vector3 origin,
            Quaternion rotation,
            IEnumerable<Triangle> triangles,
            float totalFlux,
            SpectralColor spectralColor
        ) : base(origin, rotation, triangles)
        {
            TotalFlux = totalFlux;
            Spectral = spectralColor;
        }

        public float TotalFlux { get; }
        public SpectralColor Spectral { get; }
    }
}