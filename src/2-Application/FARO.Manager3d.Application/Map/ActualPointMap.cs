using AutoMapper;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Application.Map
{
    public class ActualPointMap: Profile
    {
        public ActualPointMap()
        {
            CreateMap<ActualPoint, ActualPointViewModel>();
            CreateMap<ActualPointViewModel, ActualPoint>();
            
        }
    }
}