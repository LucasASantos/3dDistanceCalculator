using NetDevPack.Domain;

namespace FARO.Manager3d.Domain.Models
{
    public abstract class Point: Entity
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}