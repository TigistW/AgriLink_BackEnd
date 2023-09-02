using Application.Contracts;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AgriLinkDbContext _context;

    private IFarmRepository _farmRepository;


    public UnitOfWork(AgriLinkDbContext context)
    {
        _context = context;
    }
    public IFarmRepository FarmRepository
    {
        get
        {
            return _farmRepository = new FarmRepository(_context);
        }
    }



    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }
}
