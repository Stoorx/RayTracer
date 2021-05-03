using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace RayTracerCs
{
    class Program
    {
        static void Main(string[] args)
        {
            var camera = new Camera(new Vector3(0f, 0f, -1f), Quaternion.Identity, new Vector3(1f, 1f, 1f));
            var lights = new LightSource(
                new Vector3(1f, 1f, -1f),
                Quaternion.Identity,
                new List<Triangle>()
                {
                    new Triangle(new Vector3(-0.5f, 0.5f, 0f), new Vector3(0.5f, 0.5f, 0f),
                        new Vector3(-0.5f, -0.5f, 0f)),
                    new Triangle(new Vector3(0.5f, 0.5f, 0f), new Vector3(0.5f, -0.5f, 0f),
                        new Vector3(-0.5f, -0.5f, 0f))
                },
                100f,
                new SpectralColor(new float[40].Select((f, i) => (float) i * 10).ToArray()).Normalize());
            var objects = new MaterialObject(
                new Vector3(0f, 0f, 0f),
                Quaternion.CreateFromYawPitchRoll(0.5f, 0.5f, 0f),
                new List<Triangle>()
                {
                    new Triangle(new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0)),
                    new Triangle(new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0)),

                    // new Triangle(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0)),
                    // new Triangle(new Vector3(1, 1, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0))
                },
                SpectralColor.One()
            );
            var scene = new Scene(new List<MaterialObject>() {objects}, new List<LightSource>() {lights});

            var renderer = new Renderer(scene, camera);
            var image = renderer.Render(1366, 768, 4);
            var imgStr = HDRPrinter.StringifyImage(image);
            File.WriteAllText("C:\\img.txt", imgStr);
        }
    }
}