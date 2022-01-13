using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Data.Context;
using FARO.Manager3d.Domain.Models;
using FARO.Manager3d.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FARO.Manager3d.Data.Repository
{
    public class ActualPointRepository : BaseRepository<ActualPoint>, IActualPointRepository
    {
        public ActualPointRepository(ApplicationContext context): base(context){}

        public override void Add(ActualPoint obj)
        {
            base.Db.Entry(obj.NominalPoint).State = EntityState.Unchanged;
            base.Add(obj);
        }

        public async Task<IEnumerable<ActualPoint>> GetByNominalIdAsync(Guid nominalId, CancellationToken cancellationToken)
        {
            return await base.DbSet.AsNoTracking().Where(a => a.NominalPoint.Id == nominalId).ToListAsync();
        }
    }
}