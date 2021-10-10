namespace MandelbrotGenerator.Models
{
    public class Boundary<T>
    {
        public T MinX { get; set; }
        public T MaxX { get; set; }
        public T MinY { get; set; }
        public T MaxY { get; set; }
    }

    public class WindowBoundary : Boundary<int> { }

    public class DomainBoundary : Boundary<double> { }
}
