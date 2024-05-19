using TestCoreApp.Models;

namespace TestCoreApp.Repo.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepo<Category> categorries { get; }
        IRepo<Item> items { get; }
        IEmpRepo employees { get; }

        int CommitChenges();
    }
}
