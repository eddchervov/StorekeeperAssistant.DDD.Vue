using StorekeeperAssistant.Domain.MovingAggregate;
using System.Threading.Tasks;

namespace StorekeeperAssistant.UseCases.Interfaces
{
    public interface IMovingRepository
    {
        void Add(Moving moving);
        Task SaveAsync();
    }
}
