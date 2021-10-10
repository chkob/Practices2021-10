namespace MandelbrotGenerator.Models
{
    public class CallingParam
    {
        public int MaxIteration { get; set; }
        public bool SmoothColor { get; set; }
        public WindowBoundary Window { get; set; }
        public DomainBoundary Domain { get; set; }
    }
}
