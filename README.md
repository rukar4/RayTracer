# Ray Tracer

A .NET command-line ray tracing application that parses scene description files and outputs PPM images. 
The ray tracer supports simple spheres and triangles, and is able to render reflections and shadows.

## How to use

To run the application, use the following structure:

```
dotnet run [--no-shadows] <output directory> <scene1.txt> <scene2.txt> ...
```

After creating the PPM file, it is generally helpful to convert to a more accessible form of media.
Using another application such as [ImageMagick](https://imagemagick.org) is recommended to convert to `.jpg` or `.png`.

> Note: The `--no-shadows` tag prevents the tracer from sending shadow rays from props to see if it is in shadow.
This means that shadows from other props will not appear; however, shading on a prop will still be visible as that is calculated using light rays from the scene.

---

### Scene File Format

The scene description file is a plain text file with one keyword per line, followed by any associated values. Keywords must be properly capitalized and appear in the expected order. Comments can be added using `#` at the beginning of a line. Blank lines are ignored.

---

#### Global Scene Settings

These must appear before any object definitions:

| Keyword            | Description                                   | Example                       |
| ------------------ | --------------------------------------------- | ----------------------------- |
| `OutputFile`       | Output filename for the rendered image        | `OutputFile scene1.ppm`       |
| `CameraLookAt`     | The point the camera is looking at            | `CameraLookAt 0 0 0`          |
| `CameraCenter`     | The position of the camera                    | `CameraCenter 0 0 10`         |
| `CameraUp`         | The hint vector for the upward direction      | `CameraUp 0 1 0`              |
| `FieldOfView`      | Vertical field of view in degrees             | `FieldOfView 90`              |
| `DirectionToLight` | Directional light vector (will be normalized) | `DirectionToLight -1 1 -1`    |
| `LightColor`       | RGB color of the light source                 | `LightColor 1.0 1.0 1.0`      |
| `AmbientLight`     | RGB ambient light color                       | `AmbientLight 0.1 0.1 0.1`    |
| `BackgroundColor`  | RGB background color                          | `BackgroundColor 0.2 0.2 0.2` |

---

### Objects

Each object block starts with its type, followed by relevant properties. Supported object types are:

---

#### `Sphere`

| Keyword  | Description                      | Example              |
| -------- | -------------------------------- | -------------------- |
| `Sphere` | Begins a sphere block            | `Sphere`             |
| `Center` | Sphere center position (x, y, z) | `Center 0.0 0.0 0.0` |
| `Radius` | Sphere radius                    | `Radius 0.4`         |

**Material Properties (optional but recommended):**

| Keyword | Description                     | Example          |
| ------- | ------------------------------- | ---------------- |
| `Kd`    | Diffuse reflection coefficient  | `Kd 0.7`         |
| `Ks`    | Specular reflection coefficient | `Ks 0.2`         |
| `Ka`    | Ambient reflection coefficient  | `Ka 0.1`         |
| `Kgls`  | Specular exponent (shininess)   | `Kgls 16.0`      |
| `Od`    | RGB diffuse color               | `Od 1.0 0.0 1.0` |
| `Os`    | RGB specular color              | `Os 1.0 1.0 1.0` |
| `Refl`  | Reflection intensity (0 to 1)   | `Refl 0.5`       |

---

#### `Triangle`

| Keyword    | Description                              | Example                                              |
| ---------- | ---------------------------------------- | ---------------------------------------------------- |
| `Triangle` | Begins a triangle block (3 lines follow) | `Triangle`                                           |
| *3 lines*  | Each line contains a vertex (x, y, z)    | `0.0 -0.7 -0.5`<br>`1.0 0.4 -1.0`<br>`0.0 -0.7 -1.5` |

**Material Properties**: same as `Sphere`

---

### Notes

* Material properties must appear *after* their object is declared.
* If multiple objects are declared, each must be separated by a `Sphere` or `Triangle` keyword.
* You can mix spheres and triangles freely.
* The last object must also have its material properties set **before the end of the file**, otherwise it may not be added to the scene.

