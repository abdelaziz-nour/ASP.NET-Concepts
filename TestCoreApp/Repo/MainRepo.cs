using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestCoreApp.Data;
using TestCoreApp.Repo.Base;

namespace TestCoreApp.Repo
{
    public class MainRepo<T> : IRepo<T> where T : class
    {
        public MainRepo(AppDbContext context)
        {
            this.context = context;
        }
        protected AppDbContext context { get; set; }

        T IRepo<T>.FindById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IEnumerable<T> FindAll()
        {
            return context.Set<T>().ToList();
        }

        public T SelectOne(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().SingleOrDefault(expression);
        }

        async Task<T> IRepo<T>.FindByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        async Task<IEnumerable<T>> IRepo<T>.FindAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        IEnumerable<T> IRepo<T>.FindAll(params string[] agers)
        {
            IQueryable<T> query = context.Set<T>();
            if (agers != null)
            {
                foreach (var ager in agers)
                {
                    query = query.Include(ager);
                }
            }
            return query.ToList();
        }

        async Task<IEnumerable<T>> IRepo<T>.FindAllAsync(params string[] agers)
        {
            IQueryable<T> query = context.Set<T>();
            if (agers != null)
            {
                foreach (var ager in agers)
                {
                    query = query.Include(ager);
                }
            }
            return await query.ToListAsync();
        }

        //=====================================================================//

        void IRepo<T>.addOne(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        void IRepo<T>.updateOne(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        void IRepo<T>.deleteOne(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        void IRepo<T>.addMany(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }

        void IRepo<T>.updateMany(IEnumerable<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
            context.SaveChanges();
        }

        void IRepo<T>.deleteMany(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            context.SaveChanges();
        }
    }
}
