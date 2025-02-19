using System;

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

class RayTracer {
    static void Main() {
        
    }
}