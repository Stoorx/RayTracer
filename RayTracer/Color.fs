module rec RayTracer.Color

type SpectralColor = { values: float array }

[<Struct>]
type RGBColor<'T> = { R: 'T; G: 'T; B: 'T }

type RGBColorFloat = RGBColor<float>
type RGBColorByte = RGBColor<byte>

module SpectralColor =
    let initZero: SpectralColor = { values = Array.zeroCreate 40 }

    let initOne: SpectralColor = { values = Array.create 40 1.0 }

    let initFromArray (array: float array): SpectralColor =
        assert (array.Length = 40)
        { values = downcast array.Clone() }

    let initEqualEnergetic: SpectralColor =
        { values = Array.init 40 (fun _ -> 0.025) }

    let initFromTemperature (temperature: float): SpectralColor =
        { values =
              Array.init 40 (fun i ->
                  let wavelength = 10 * i + 380
                  double wavelength) }

    let toRGB (spectral: SpectralColor): RGBColorFloat =
        spectral.values
        |> Array.splitInto 3
        |> Array.map (fun c -> c |> Array.sum)
        |> fun a ->
            { R = (a.[2])
              G = (a.[1])
              B = (a.[0]) }

    let toRGB8 (spectral: SpectralColor): RGBColorByte =
        let c =
            RGBColorFloat.toRGBNormalized (toRGB spectral)

        { R = byte (c.R * 255.0)
          G = byte (c.G * 255.0)
          B = byte (c.B * 255.0) }

module RGBColorFloat =
    let toRGBNormalized (color: RGBColorFloat): RGBColorFloat =
        let max = max (max color.R color.G) color.B

        { R = color.R / max
          G = color.G / max
          B = color.B / max }
