using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FARO.Manager3d.Application.Service.Interfaces;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Application.Service
{
    public class ActualPointAppService : IActualPointAppService
    {
        private readonly IActualDomainService _actualDomainService;
        private readonly IMapper _mapper;

        public ActualPointAppService(IActualDomainService actualDomainService, IMapper mapper)
        {
            _actualDomainService = actualDomainService;
            _mapper = mapper;
        }

        public async Task<ActualPointViewModel> AddAsync(double x, double y, double z, NominalPointViewModel nominalPoint, CancellationToken cancellationToken)
        {
            var actualPoint = await _actualDomainService.CreateNewAsync(x, y, z, _mapper.Map<NominalPoint>(nominalPoint), cancellationToken);
            await _actualDomainService.AddAsync(actualPoint, cancellationToken);
            return _mapper.Map<ActualPointViewModel>(actualPoint);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var actualPoint = await _actualDomainService.GetByIDAsync(id, cancellationToken);
            await _actualDomainService.DeleteAsync(actualPoint, cancellationToken);
        }

        public async Task<IEnumerable<ActualPointViewModel>> GetAsync(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ActualPointViewModel>>(await _actualDomainService.GetAsync(cancellationToken));
        }

        public async Task<ActualPointViewModel> GetByIDAsync(Guid id, CancellationToken cancellationToken)
        {
            return _mapper.Map<ActualPointViewModel>(await _actualDomainService.GetByIDAsync(id, cancellationToken));
        }

        public async Task<IEnumerable<ActualPointViewModel>> GetByNominalPointAsync(Guid nominalPointId, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ActualPointViewModel>>(await _actualDomainService.GetByNominalPointAsync(nominalPointId, cancellationToken));
        }

        public async Task<ActualPointViewModel> UpdateAsync(Guid id, double x, double y, double z,  CancellationToken cancellationToken)
        {
            var actualPoint = await _actualDomainService.GetByIDAsync(id, cancellationToken);
            actualPoint.X = x;
            actualPoint.Y = y;
            actualPoint.Z = z;

            await _actualDomainService.UpdateAsync(actualPoint, cancellationToken);

            return _mapper.Map<ActualPointViewModel>(actualPoint);
        }
    }
}