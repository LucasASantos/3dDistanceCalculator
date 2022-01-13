using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;
using FARO.Manager3d.Domain.Repository.Interfaces;
using FluentValidation;

namespace FARO.Manager3d.Domain.DomainService
{
    public class ActualDomainService : IActualDomainService
    {
        private readonly IValidator<ActualPoint> _validator;
        private readonly IActualPointRepository _actualPointRepository;

        public ActualDomainService(IValidator<ActualPoint> validator, IActualPointRepository actualPointRepository)
        {
            _validator = validator;
            _actualPointRepository = actualPointRepository;
        }

        public async Task<ActualPoint> CreateNewAsync(double x, double y, double z, NominalPoint nominalPoint, CancellationToken cancellationToken)
        {
            var actualPoint = new ActualPoint() { X = x, Y = y, Z = z, NominalPoint = nominalPoint };
            await _validator.ValidateAndThrowAsync(actualPoint);
            return actualPoint;
        }

        public async Task AddAsync(ActualPoint actualPoint, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(actualPoint);
            _actualPointRepository.Add(actualPoint);
            await _actualPointRepository.SaveAsync(cancellationToken);

        }

        public async Task UpdateAsync(ActualPoint actualPoint, CancellationToken cancellationToken)
        {
            _actualPointRepository.Update(actualPoint);
            await _actualPointRepository.SaveAsync(cancellationToken);
        }

        public async Task DeleteAsync(ActualPoint actualPoint, CancellationToken cancellationToken)
        {
            _actualPointRepository.Delete(actualPoint);
            await _actualPointRepository.SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<ActualPoint>> GetAsync(CancellationToken cancellationToken)
        {
            return await _actualPointRepository.GetAsync(cancellationToken);
        }

        public async Task<ActualPoint> GetByIDAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _actualPointRepository.GetByIDAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<ActualPoint>> GetByNominalPointAsync(Guid nominalPointId, CancellationToken cancellationToken)
        {
            return await _actualPointRepository.GetByNominalIdAsync(nominalPointId, cancellationToken);

        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}