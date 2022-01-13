
using System.Collections.Generic;

namespace FARO.Manager3d.Domain.Models
{
    public class NominalPoint: Point
    {
        public NominalPoint(){}
        
        public List<ActualPoint> ActualPoints { get; set; }
    }
}