using System;
using System.IO;

public class Vector {
    public float x;
    public float y;
    public float z;

    public Vector(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Add(Vector vector) {
        x += vector.x;
        y += vector.y;
        z += vector.z;
    }

    public void Update(Vector vector) {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }
    
    public Vector GetSum(Vector vector) {
        return new Vector(x + vector.x, y + vector.y, z + vector.z);
    }
}

public class Ray {
    public Vector origin;
    public Vector dir;
    public Vector dest;

    public Ray(float x0, float y0, float z0, float xd, float yd, float zd) {
        origin = new Vector(x0, y0, z0);
        dir = new Vector(xd, yd, zd);
        dest = origin.GetSum(dir);
    }
}

public class PPMWriter {
    private string PPM_TYPE = "P3";
    private int MAX_COLOR_VAL = 255;

    private StreamWriter writer;
    private string file;
    private int height;
    private int width;

    public PPMWriter(string file, int height, int width) {
        this.file = file;
        this.height = height;
        this.width = width;
        writer = new StreamWriter(file);

        // Initialize file
        writer.WriteLine(PPM_TYPE);
        writer.WriteLine($"{width} {height}");
        writer.WriteLine($"{MAX_COLOR_VAL}");
    }

    public void WriteRGB(int r, int g, int b) {
        writer.Write($"{r} {g} {b}\t");
    }

    public void WriteLine() {
        writer.WriteLine();
    }

    public void Dispose() {
        writer?.Close();
        writer?.Dispose();
    }
}

class RayTracer {
    static void Main() {
        int width = 256;
        int height = 256;

        PPMWriter writer = new PPMWriter("sphere.ppm", height, width);

        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                double r = (double) i / (width - 1);
                double g = (double) j / (height - 1);
                double b = 0.0;

                int ir = (int) (255.999 * r);
                int ig = (int) (255.999 * g);
                int ib = (int) (255.999 * b);
                
                writer.WriteRGB(ir, ig, ib);
            }
            writer.WriteLine();
        }

        writer.Dispose();
    }
}