using Models.Props;

public class Sphere : Prop {
    // Geometry
    protected readonly double r;
    protected readonly Vector center;

    public Sphere(double r, double x = 0, double y = 0, double z = 0) {
        this.r = r;
        center = new Vector(x, y, z);
    }

    public override Vector GetNormal(Vector point) {
        return (point - center).UnitVector();
    }

    public override Vector? GetIntersection(Ray ray) {
        Vector OC = center - ray.origin;
        double t_ca = ray.dir.Dot(OC);

        // Ray is pointing away from sphere
        if (t_ca < 0) {
            return null;
        }

        // Use squared value to compute t_hc squared using Pythagorean's Theorem.
        double magOC2 = OC.LengthSqrd();
        double t_hc2 = r * r - magOC2 + t_ca * t_ca;

        // Ray misses sphere since t_hc = 0 implies the ray is tangent.
        if (t_hc2 < 0) {
            return null;
        }

        // Check if the point is inside the sphere and calculate t for finding the intersection. 
        // If the length of OC < r, then it must be in the sphere.
        double t;
        double t_hc = Math.Sqrt(t_hc2);
        if (Math.Sqrt(magOC2) < r) {
            t = t_ca + t_hc;
        } else {
            t = t_ca - t_hc;
        }

        return ray.At(t);
    }
}