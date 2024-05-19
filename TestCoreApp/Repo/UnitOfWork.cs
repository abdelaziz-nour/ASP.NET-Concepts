using TestCoreApp.Data;
using TestCoreApp.Models;
using TestCoreApp.Repo.Base;

namespace TestCoreApp.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            categorries = new MainRepo<Category>(_context);
            items = new MainRepo<Item>(_context);
            employees = new EmpRepo(_context);
        }
        private readonly AppDbContext _context;

        public IRepo<Category> categorries { get; private set; }
        public IRepo<Item> items { get; private set; }
        public IEmpRepo employees { get; private set; }

        int IUnitOfWork.CommitChenges()
        {
            return _context.SaveChanges();
        }

        void IDisposable.Dispose()
        {
            _context.Dispose();
        }
    }
}
