module RayTracer.Ray

open System.Numerics
open Color

type Ray =
    { origin: Vector3
      direction: Vector3
      color: SpectralColor
      reflectionRemains : uint }

module Ray =
    let at (ray: Ray) (distance: float32): Vector3 = ray.direction * distance + ray.origin

    let reflect (ray: Ray) (normal: Vector3) (point: Vector3) (colorFilter: SpectralColor): Ray =
        { origin = point
          direction = Vector3.Reflect(ray.direction, normal)
          color = Array.map2 (*) (ray.color.values) (colorFilter.values) |> SpectralColor.initFromArray
          reflectionRemains = ray.reflectionRemains - 1u}
