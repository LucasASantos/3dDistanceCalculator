using FARO.Manager3d.Application.Tasks.Interfaces;

namespace FARO.Manager3d.Application.Factory.Interfaces
{
    public interface IDistanceCalculatorFactory
    {
         IDistanceCalculator GetCalculator(string method);
    }
}