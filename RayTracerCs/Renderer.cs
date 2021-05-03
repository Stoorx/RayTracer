using System;
using System.Linq;
using System.Numerics;

namespace RayTracerCs
{
    public class Renderer
    {
        public Renderer(Scene scene, Camera camera)
        {
            Scene = scene;
            Camera = camera;
        }

        public Scene Scene { get; }
        public Camera Camera { get; }

        public Image Render(uint w, uint h)
        {
            var image = new Image(w, h);
            var allTriangles = Scene.Objects.SelectMany(
                o => o.TranslateToWorld(), (_, triangle) => triangle
            ).ToArray();

            for (uint i = 0; i < w; i++)
            {
                for (uint j = 0; j < h; j++)
                {
                    var ray = Camera.CastRay(new Vector2(2.0f * ((float) i / w) - 1f, -(2.0f * ((float) j / h) - 1f)));
                    var (firstHittedTriangle, hitPoint) =
                        allTriangles
                            .Select(triangle => new Tuple<Triangle, Vector3?>(triangle, triangle.Hit(ray)))
                            .Where(hit => hit.Item2.HasValue)
                            .Select(hit => new Tuple<Triangle, Vector3>(hit.Item1, hit.Item2.Value))
                            .OrderBy(hit => (hit.Item2 - Camera.Origin).LengthSquared())
                            .First();

                    var triangleOwner = firstHittedTriangle.Owner;
                    switch (triangleOwner)
                    {
                        case MaterialObject owner:
                        {
                            var shadowRays =
                                Scene.Lights
                                    .Select(source => new Tuple<LightSource, Vector3>(source, source.Origin - hitPoint))
                                    .Select(
                                        shadowVector => new Tuple<LightSource, Ray>(shadowVector.Item1,
                                            new Ray(hitPoint, Vector3.Normalize(shadowVector.Item2), owner.Color))
                                    )
                                    .Where(
                                        shadowRay => allTriangles
                                            .Select(
                                                t => t.Hit(shadowRay.Item2)).All(o => o.HasValue == false)
                                    );

                            var lums = shadowRays.Select(r =>
                                r.Item1.Luminance((hitPoint - r.Item1.Origin).LengthSquared())
                                    .ApplyFilter(r.Item2.ColorFilter)
                                    .Values.Select(
                                        v =>
                                            v * (Vector3.Dot(firstHittedTriangle.Normal(), r.Item2.Direction))
                                    ).ToArray()
                            ).ToArray();
                            var res = new float[40];
                            foreach (var s in lums)
                            {
                                for (int k = 0; k < 40; k++)
                                {
                                    res[k] += s[k];
                                }
                            }

                            image.AccumulatePixel(i, j, new SpectralColor(res));
                            break;
                        }
                        case LightSource owner:
                        {
                            image.AccumulatePixel(i, j, owner.Luminance((Camera.Origin - hitPoint).LengthSquared()));
                            break;
                        }
                    }
                }
            }

            return image;
        }
    }
}