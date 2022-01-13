using AutoMapper;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Application.Map
{
    public class NominalPointMap: Profile
    {
        public NominalPointMap()
        {
            CreateMap<NominalPoint, NominalPointViewModel>();
            CreateMap<NominalPointViewModel, NominalPoint>();
        }
    }
}