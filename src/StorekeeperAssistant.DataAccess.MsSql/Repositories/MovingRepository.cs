using StorekeeperAssistant.Domain.Movings;
using StorekeeperAssistant.UseCases.Interfaces;
using System.Threading.Tasks;

namespace StorekeeperAssistant.DataAccess.Repositories;

public sealed class MovingRepository : IMovingRepository
{
    private readonly AppDbContext _context;

    public MovingRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Moving moving)
    {
        _context.Movings.Add(moving);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
