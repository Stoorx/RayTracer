module RayTracer.Geometry

open System.Numerics
open Ray

type Triangle = { A: Vector3; B: Vector3; C: Vector3 }

module Triangle =
    let init a b c = { A = a; B = b; C = c }

    let cross (triangle: Triangle): Vector3 =
        (triangle.B - triangle.A, triangle.C - triangle.A)
        |> Vector3.Cross

    let normal (triangle: Triangle) = triangle |> cross |> Vector3.Normalize
    let square (triangle: Triangle) = triangle |> cross |> fun v -> v.Length()

    let hit (triangle: Triangle) (ray: Ray): Option<Vector3> =
        let epsilon = 1e-9f
        let e1 = triangle.B - triangle.A
        let e2 = triangle.C - triangle.A
        let pvec = Vector3.Cross(ray.direction, e2)
        let det = Vector3.Dot(e1, pvec)

        if det < epsilon && det > -epsilon then
            None
        else
            let invdet = 1f / det
            let tvec = ray.origin - triangle.A
            let u = (Vector3.Dot(tvec, pvec)) * invdet

            if u < 0f || u > 1f then
                None
            else
                let qvec = Vector3.Cross(tvec, e1)

                let v =
                    (Vector3.Dot(ray.direction, qvec)) * invdet

                if v < 0f || u + v > 1f then
                    None
                else
                    let t = Vector3.Dot(e2, qvec) * invdet
                    if t <= 0f then None else Ray.at ray t |> Some
