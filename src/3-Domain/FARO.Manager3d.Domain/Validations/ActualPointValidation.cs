using FARO.Manager3d.Domain.Models;
using FluentValidation;

namespace FARO.Manager3d.Domain.Validations
{
    public class ActualPointValidation : AbstractValidator<ActualPoint>
    {
        public ActualPointValidation()
        {
            RuleFor(c => c.NominalPoint)
                .NotNull();
            
            
        }
    }
}