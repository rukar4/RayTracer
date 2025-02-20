public class Vector {
    public double x;
    public double y;
    public double z;

    public Vector(double x = 0, double y = 0, double z = 0) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Update(Vector vector) {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public void Normalize() {
        Vector unit = UnitVector();
        x = unit.x;
        y = unit.y;
        z = unit.z;
    }

    public double Length() {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public double LengthSqrd() {
        return x * x + y * y + z * z;
    }

    public Vector UnitVector() {
        double length = Length();
        return length > 0 ? new Vector(x / length, y / length, z / length) : new Vector(0, 0, 0);
    }

    // Dot product method
    public double Dot(Vector v) {
        return x * v.x + y * v.y + z * v.z;
    }

    public static Vector operator -(Vector v) {
        return new Vector(-v.x, -v.y, -v.z);
    }

    public static Vector operator +(Vector v, double t) {
        return new Vector(v.x + (float)t, v.y + (float)t, v.z + (float)t);
    }

    public static Vector operator +(double t, Vector v) {
        return v + t;
    }

    public static Vector operator +(Vector v1, Vector v2) {
        return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static Vector operator -(Vector v, double t) {
        return new Vector(v.x - (float)t, v.y - (float)t, v.z - (float)t);
    }

    public static Vector operator -(double t, Vector v) {
        return -v + t;
    }

    public static Vector operator -(Vector v1, Vector v2) {
        return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public static Vector operator /(Vector v, double t) {
        return new Vector(v.x / (float)t, v.y / (float)t, v.z / (float)t);
    }

    public static Vector operator *(Vector v, double t) {
        return new Vector(v.x * (float)t, v.y * (float)t, v.z * (float)t);
    }

    public static Vector operator *(double t, Vector v) {
        return v * t; // Delegate to the other overload
    }
}
