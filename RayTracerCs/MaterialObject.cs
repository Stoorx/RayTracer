using System.Collections.Generic;
using System.Numerics;

namespace RayTracerCs
{
    public class MaterialObject : TracerObject
    {
        public MaterialObject(
            Vector3 origin,
            Quaternion rotation,
            List<Triangle> triangles,
            SpectralColor color
        ) : base(origin, rotation, triangles)
        {
            Color = color;
        }

        public SpectralColor Color { get; }
    }
}