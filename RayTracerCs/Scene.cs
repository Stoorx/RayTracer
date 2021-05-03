using System.Collections.Generic;

namespace RayTracerCs
{
    public class Scene
    {
        public List<TracerObject> Objects { get; } = new();
        public List<LightSource> Lights { get; } = new();
    }
}