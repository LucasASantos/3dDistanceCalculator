using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;
using FARO.Manager3d.Domain.Repository.Interfaces;
using FluentValidation;

namespace FARO.Manager3d.Domain.DomainService
{
    public class NominalDomainService : INominalDomainService
    {
        private readonly IValidator<NominalPoint> _validator;
        private readonly INominalPointRepository _nominalPointRepository;
        private readonly IActualPointRepository _actualPointRepository;

        public NominalDomainService(IValidator<NominalPoint> validator, INominalPointRepository nominalPointRepository, IActualPointRepository actualPointRepository)
        {
            _validator = validator;
            _nominalPointRepository = nominalPointRepository;
            _actualPointRepository = actualPointRepository;
        }

        public async Task<NominalPoint> CreateNewAsync(double x, double y, double z, CancellationToken cancellationToken)
        {
            var nominalPoint = new NominalPoint() { X = x, Y = y, Z = z };
            await _validator.ValidateAndThrowAsync(nominalPoint, cancellationToken);
            return nominalPoint;
        }

        public async Task AddAsync(NominalPoint nominalPoint, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(nominalPoint, cancellationToken);
            _nominalPointRepository.Add(nominalPoint);
            await _nominalPointRepository.SaveAsync(cancellationToken);
        }

        public async Task DeleteAsync(NominalPoint nominalPoint, CancellationToken cancellationToken)
        {
            var actualPoints = await _actualPointRepository.GetByNominalIdAsync(nominalPoint.Id, cancellationToken);
            _actualPointRepository.DeleteRange(actualPoints);
            _nominalPointRepository.Delete(nominalPoint);
            await _nominalPointRepository.SaveAsync(cancellationToken);
        }

        public async Task UpdateAsync(NominalPoint nominalPoint, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(nominalPoint, cancellationToken);
            _nominalPointRepository.Update(nominalPoint);
            await _nominalPointRepository.SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<NominalPoint>> GetAsync(CancellationToken cancellationToken)
        {
            return await _nominalPointRepository.GetAsync(cancellationToken);
        }

        public async Task<NominalPoint> GetByIDAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _nominalPointRepository.GetByIDAsync(id, cancellationToken);

        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}