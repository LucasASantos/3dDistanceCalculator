using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FARO.Manager3d.Data.Context;
using FARO.Manager3d.Data.Repository;
using FARO.Manager3d.Domain.Models;
using FARO.Manager3d.Domain.Repository.Interfaces;
using FARO.Manager3d.Domain.Validations;
using FARO.Manager3d.Domain.DomainService;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Application.Factory;
using FARO.Manager3d.Application.Factory.Interfaces;
using FARO.Manager3d.Application.Service;
using FARO.Manager3d.Application.Service.Interfaces;
using FARO.Manager3d.Application.Map;

namespace FARO.Manager3d.CrossCutting.DependenceInjection
{
    public static class InjectorService
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //AutoMaper
            services.AddAutoMapper(typeof(NominalPointMap), typeof(ActualPointMap));

            //Application
            services.AddScoped<IDistanceCalculatorFactory, DistanceCalculatorFactory>();
            services.AddScoped<IActualPointAppService, ActualPointAppService>();
            services.AddScoped<INominalPointAppService, NominalPointAppService>();
            services.AddScoped<IPointAppService, PointAppService>();


            //Domain
            services.AddScoped<IValidator<NominalPoint>, NominalPointValidation>();
            services.AddScoped<IValidator<ActualPoint>, ActualPointValidation>();
            services.AddScoped<IActualDomainService, ActualDomainService>();
            services.AddScoped<INominalDomainService, NominalDomainService>();

            //Data
            services.AddScoped<ApplicationContext>();
            services.AddScoped<IActualPointRepository, ActualPointRepository>();
            services.AddScoped<INominalPointRepository, NominalPointRepository>();
        }
    }
}