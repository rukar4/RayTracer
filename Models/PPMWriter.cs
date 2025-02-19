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

    public void WriteRGB(Vector color) {
        int ir = (int) (255.999 * color.x);
        int ig = (int) (255.999 * color.y);
        int ib = (int) (255.999 * color.z);

        writer.Write($"{ir} {ig} {ib}\t");
    }

    public void WriteLine() {
        writer.WriteLine();
    }

    public void Dispose() {
        writer?.Close();
        writer?.Dispose();
    }
}
