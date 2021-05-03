module RayTracer.MaterialObject

open System.Numerics
open RayTracer.Color
open RayTracer.Geometry

type MaterialObject(origin: Vector3, rotation : Quaternion, triangles: Triangle array, colorFn: SpectralColor-> SpectralColor) =
    inherit TracerObject(origin, rotation, triangles)
    member this.colorTransformation : SpectralColor -> SpectralColor = colorFn