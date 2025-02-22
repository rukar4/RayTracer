public class Sphere {
    // Geometry
    public double r;
    public Vector center;

    // Shading and color
    double Kd = 0.0, Ks = 0.0, Ka = 0.0, Kgls = 0.0;
    Vector Od = new Vector(0, 0, 0);
    Vector Os = new Vector(1.0, 1.0, 1.0);

    public Sphere(double r, double x = 0, double y = 0, double z = 0) {
        this.r = r;
        center = new Vector(x, y, z);
    }

    public void SetColor(double Kd, double Ks, double Ka, double Kgls, Vector Od, Vector Os) {
        this.Kd = Kd;
        this.Ks = Ks;
        this.Ka = Ka;
        this.Od = Od;
        this.Os = Os;
        this.Kgls = Kgls;
    }

    public Vector GetSurfaceColor(Vector point, Vector camCenter, Scene scene) {
        Vector L = scene.GetLightDir();
        Vector N = GetNormal(point);
        Vector R = scene.LightReflection(N);
        Vector V = (camCenter - point).UnitVector();
        return  Ka * scene.GetAmbientColor() * Od + 
                Kd * scene.GetLightColor() * Od * Math.Max(0, N.Dot(L)) + 
                Ks * scene.GetLightColor() * Os * Math.Pow(Math.Max(0, R.Dot(V)), Kgls);
    }

    public Vector GetNormal(Vector point) {
        return (point - center).UnitVector();
    }
}