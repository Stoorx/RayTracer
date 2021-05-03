module RayTracer.Geometry

open System
open System.Numerics
open Ray

[<Struct>]
type TracerObject(origin: Vector3, rotation: Quaternion, triangles: Triangle array) =
    member this.origin: Vector3 = origin
    member this.rotation: Quaternion = rotation

    member this.triangles: Triangle array =
        triangles
        |> Array.map (fun t -> Triangle(t.A, t.B, t.C, this))

    member this.translateToWorld(obj: TracerObject): Triangle array =
        obj.triangles
        |> Array.map (fun t -> t.translate (obj.origin, obj.rotation))

and [<Struct>] Triangle(a: Vector3, b: Vector3, c: Vector3, ownerObj: Nullable<TracerObject>) =
    new(a: Vector3, b: Vector3, c: Vector3) = { Triangle(a, b, c, Nullable())}
    member this.A: Vector3 = a
    member this.B: Vector3 = b
    member this.C: Vector3 = c
    member this.owner: Nullable<TracerObject> = ownerObj

    member this.cross: Vector3 =
        (this.B - this.A, this.C - this.A)
        |> Vector3.Cross

    member this.normal = this.cross |> Vector3.Normalize
    member this.square = this.cross |> fun v -> v.Length()

    member this.hit(ray: Ray): Option<Vector3> =
        let epsilon = 1e-9f
        let e1 = this.B - this.A
        let e2 = this.C - this.A
        let pvec = Vector3.Cross(ray.direction, e2)
        let det = Vector3.Dot(e1, pvec)

        if det < epsilon && det > -epsilon then
            None
        else
            let invdet = 1f / det
            let tvec = ray.origin - this.A
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

    member this.translate(origin: Vector3, rotation: Quaternion): Triangle =
        let translatePoint (p: Vector3) =
            ((p, rotation) |> Vector3.Transform, origin)
            |> Vector3.Add

        Triangle(translatePoint this.A, translatePoint this.B, translatePoint this.C, this.owner)
