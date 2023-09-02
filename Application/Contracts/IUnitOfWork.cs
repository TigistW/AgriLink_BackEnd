namespace Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IFarmRepository FarmRepository{ get; }
    Task<int> Save();
}