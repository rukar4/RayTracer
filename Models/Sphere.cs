public class Sphere {
    // Geometry
    public double r;
    public Vector center;

    // Shading and color
    float Kd, Ks, Ka, Kgls;
    Vector Od = new Vector(0, 0, 0);
    Vector Os = new Vector(0, 0, 0);

    public Sphere(double r, double x = 0, double y = 0, double z = 0) {
        this.r = r;
        center = new Vector(x, y, z);
    }

    public void SetColor(float Kd, float Ks, float Ka, Vector Od, Vector Os, float Kgls) {
        this.Kd = Kd;
        this.Ks = Ks;
        this.Ka = Ka;
        this.Od = Od;
        this.Os = Os;
        this.Kgls = Kgls;
    }

    public Vector GetNormal(Vector point) {
        return (point - center) / r;
    }
}