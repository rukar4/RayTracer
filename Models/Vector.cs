public class Vector {
    public double x;
    public double y;
    public double z;

    public Vector(double x, double y, double z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Update(Vector vector) {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public static Vector operator -(Vector v) {
        return new Vector(-v.x, -v.y, -v.z);
    }

    public static Vector operator +(Vector v1, Vector v2) {
        return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static Vector operator *(Vector v, double t) {
        return new Vector(v.x * (float)t, v.y * (float)t, v.z * (float)t);
    }

    public static Vector operator *(double t, Vector v) {
        return v * t; // Delegate to the other overload
    }

    public static Vector operator -(Vector v1, Vector v2) {
        return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public Vector Subtract(Vector vector) {
        return new Vector(x - vector.x, y - vector.y, z - vector.z);
    }

    public Vector Multiply(Vector vector) {
        return new Vector(x * vector.x, y * vector.y, z * vector.z);
    }
    
    public Vector GetSum(Vector vector) {
        return new Vector(x + vector.x, y + vector.y, z + vector.z);
    }
}
