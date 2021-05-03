module RayTracer.Scene

open TracerObject

type SceneObject =
    | TracerObject
    | LightSource
    
type Scene = {
    objects : TracerObject array
}