using Application.Contracts;
using Domain;

namespace Persistence.Repositories;

public class FarmRepository : GenericRepository<Farm>, IFarmRepository
{
    private readonly AgriLinkDbContext _dbContext;

    public FarmRepository(AgriLinkDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}