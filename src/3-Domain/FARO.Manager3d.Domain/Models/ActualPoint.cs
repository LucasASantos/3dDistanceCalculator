

namespace FARO.Manager3d.Domain.Models
{
    public class ActualPoint: Point
    {
        public ActualPoint(){}
        public NominalPoint NominalPoint { get; set; }
        public double Distance { get; set; }
        
    }
}