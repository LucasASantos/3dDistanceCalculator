using FARO.Manager3d.Data.Context;
using FARO.Manager3d.Domain.Models;
using FARO.Manager3d.Domain.Repository.Interfaces;

namespace FARO.Manager3d.Data.Repository
{
    public class NominalPointRepository :BaseRepository<NominalPoint>, INominalPointRepository
    {
        public NominalPointRepository(ApplicationContext context): base(context){}
    }
}