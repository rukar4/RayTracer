namespace Models.Props
{

    public abstract class Prop
    {
        protected int MAX_DEPTH = 5;

        protected double kd = 0.0, ks = 0.0, ka = 0.0, kgls = 0.0, refl = 0.0;
        protected Vector od = new Vector(0, 0, 0);  // Diffuse color
        protected Vector os = new Vector(1.0, 1.0, 1.0);  // Specular color

        public abstract Vector GetNormal(Vector point);
        public abstract Vector? GetIntersection(Ray ray);

        public Vector GetSurfaceColor(Vector point, Vector origin, Scene scene, int depth = 0)
        {
            Vector L = scene.GetLightDir();

            Vector N = GetNormal(point);
            Vector R = scene.LightReflection(N);
            Vector V = (origin - point).UnitVector();

            Vector color =
                ka * scene.GetAmbientColor() * od +
                kd * scene.GetLightColor() * od * Math.Max(0, N.Dot(L)) +
                ks * scene.GetLightColor() * os * Math.Pow(Math.Max(0, R.Dot(V)), kgls);

            // Shadow detection
            Ray shadowRay = new Ray(point, L);
            foreach (Prop prop in scene.GetProps())
            {
                if (prop != this && prop.GetIntersection(shadowRay) != null)
                {
                    color *= 0.5;
                    break;
                }
            }

            if (refl == 0 || depth == MAX_DEPTH)
            {
                return color;
            }
            else
            {
                Vector reflColor = scene.GetBG();
                Vector reflDir = -V.Reflect(N).UnitVector();
                Ray reflRay = new Ray(point, reflDir);

                Vector? closestInter = null;
                Prop? closestProp = null;
                double closest = double.MaxValue;

                foreach (Prop prop in scene.GetProps())
                {
                    if (prop != this)
                    {
                        Vector? intersection = prop.GetIntersection(reflRay);
                        if (intersection != null)
                        {
                            double distance = (intersection - point).Magnitude();
                            if (distance < closest)
                            {
                                closest = distance;
                                closestInter = intersection;
                                closestProp = prop;
                            }
                        }
                    }
                }

                if (closestInter != null && closestProp != null)
                {
                    reflColor = closestProp.GetSurfaceColor(closestInter, reflRay.origin, scene, depth + 1);
                }

                return color + reflColor * refl;
            }
        }

        // Set all color properties at once
        public void SetColor(double kd, double ks, double ka, double kgls, Vector od, Vector os)
        {
            this.kd = kd;
            this.ks = ks;
            this.ka = ka;
            this.kgls = kgls;
            this.od = od;
            this.os = os;
        }

        // Individual getters/setters
        public double Kd
        {
            get => kd;
            set => kd = value;
        }

        public double Ks
        {
            get => ks;
            set => ks = value;
        }

        public double Ka
        {
            get => ka;
            set => ka = value;
        }

        public double Kgls
        {
            get => kgls;
            set => kgls = value;
        }

        public double Refl
        {
            get => refl;
            set => refl = value;
        }

        public Vector Od
        {
            get => od;
            set => od = value;
        }

        public Vector Os
        {
            get => os;
            set => os = value;
        }
    }
}
