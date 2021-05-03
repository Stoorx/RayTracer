module RayTracer.LightSource

open System.Numerics
open RayTracer.Color
open RayTracer.Geometry

type LightSource(origin: Vector3, rotation : Quaternion, triangles: Triangle array, totalFlux : float32, spectral : SpectralColor) =
    inherit TracerObject(origin, rotation, triangles)
    member this.flux: float32 = totalFlux
    member this.spectral: SpectralColor = spectral
