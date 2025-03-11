using Models.Props;

public class Triangle : Prop {
    protected readonly Vector v0, v1, v2;
    protected readonly Vector edge0, edge1, edge2;

    protected readonly Vector N;
    protected readonly double d;

    public Triangle(Vector v0, Vector v1, Vector v2) {
        this.v0 = v0;
        this.v1 = v1;
        this.v2 = v2;

        N = (v1 - v0).Cross(v2 - v0).UnitVector();
        d = -N.Dot(v0);

        edge0 = v1 - v0;
        edge1 = v2 - v1;
        edge2 = v0 - v2;
    }

    public override Vector GetNormal(Vector point) {
        return N;
    }

    public override Vector? GetIntersection(Ray ray) {
        double rdDotN = ray.dir.Dot(N);

        // Check if ray is parallel
        if (Math.Abs(rdDotN) < 1e-6) {
            return null;
        }

        double t = -(ray.origin.Dot(N) + d) / rdDotN;

        // Intersection point is behind the ray
        if (t <= 0) {
            return null;
        }

        // Compute ray-plane intersection
        Vector P = ray.At(t);

        Vector C0 = P - v0;
        Vector C1 = P - v1;
        Vector C2 = P - v2;

        if (edge0.Cross(C0).Dot(N) >= 0 &&
            edge1.Cross(C1).Dot(N) >= 0 &&
            edge2.Cross(C2).Dot(N) >= 0) {
            return P;
        }

        return null;
    }
}