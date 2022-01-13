using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FARO.Manager3d.Application.Service.Interfaces;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Domain.DomainService.Interfaces;

namespace FARO.Manager3d.Application.Service
{
    public class NominalPointAppService : INominalPointAppService
    {
        private readonly INominalDomainService _nominalDomainService;
        private readonly IMapper _mapper;


        public NominalPointAppService(INominalDomainService nominalDomainService, IMapper mapper)
        {
            _nominalDomainService = nominalDomainService;
            _mapper = mapper;
        }

        public async Task<NominalPointViewModel> AddAsync(double x, double y, double z, CancellationToken cancellationToken)
        {
            var nominalPoint = await _nominalDomainService.CreateNewAsync(x, y, z, cancellationToken);
            await _nominalDomainService.AddAsync(nominalPoint, cancellationToken);
            return _mapper.Map<NominalPointViewModel>(nominalPoint);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var nominalPoint = await _nominalDomainService.GetByIDAsync(id, cancellationToken);
            await _nominalDomainService.DeleteAsync(nominalPoint, cancellationToken);
        }

        public async Task<IEnumerable<NominalPointViewModel>> GetAsync(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<NominalPointViewModel>>(await _nominalDomainService.GetAsync(cancellationToken));
        }

        public async Task<NominalPointViewModel> GetByIDAsync(Guid id, CancellationToken cancellationToken)
        {
            return _mapper.Map<NominalPointViewModel>(await _nominalDomainService.GetByIDAsync(id, cancellationToken));
        }

        public async Task<NominalPointViewModel> UpdateAsync(Guid id, double x, double y, double z, CancellationToken cancellationToken)
        {
            var nominalPoint = await _nominalDomainService.GetByIDAsync(id, cancellationToken);
            nominalPoint.X = x;
            nominalPoint.Y = y;
            nominalPoint.Z = z;

            await _nominalDomainService.UpdateAsync(nominalPoint, cancellationToken);
            return _mapper.Map<NominalPointViewModel>(nominalPoint);
        }
    }
}