using System.Linq.Expressions;

namespace TestCoreApp.Repo.Base
{
    public interface IRepo<T> where T : class
    {
        T FindById(int id);
        T SelectOne(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAll(params string[] agers);
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindAllAsync(params string[] agers);
        void addOne(T entity);
        void updateOne(T entity);
        void deleteOne(T entity);
        void addMany(IEnumerable<T> entities);
        void updateMany(IEnumerable<T> entities);
        void deleteMany(IEnumerable<T> entities);

    }
}
