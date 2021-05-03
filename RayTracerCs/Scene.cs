using System.Collections.Generic;

namespace RayTracerCs
{
    public class Scene
    {
        public Scene(List<MaterialObject> objects, List<LightSource> lights)
        {
            Objects.AddRange(objects);
            Objects.AddRange(lights);
            Lights.AddRange(lights);
        }

        public List<TracerObject> Objects { get; } = new();
        public List<LightSource> Lights { get; } = new();
    }
}