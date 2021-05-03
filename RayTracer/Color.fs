module rec RayTracer.Color

type SpectralColor = { values: float32 array }

module SpectralColor =
    let initZero: SpectralColor = { values = Array.zeroCreate 40 }

    let initOne: SpectralColor = { values = Array.create 40 1.0f }

    let initFromArray (array: float32 array): SpectralColor =
        assert (array.Length = 40)
        { values = downcast array.Clone() }

    let initEqualEnergetic: SpectralColor =
        { values = Array.init 40 (fun _ -> 0.025f) }

    //    let initFromTemperature (temperature: float): SpectralColor =
//        { values =
//              Array.init 40 (fun i ->
//                  let wavelength = 10 * i + 380
//                  float32 wavelength) }

    let addLuminance (left: SpectralColor) (right: SpectralColor): SpectralColor =
        let lv = left.values
        let rv = right.values
        { values = Array.map2 (fun l r -> l + r) lv rv }
        
    let normalize (color : SpectralColor): SpectralColor =
        let sum = Array.sum color.values
        { values = color.values |> Array.map ((/) sum) }
