using System.Linq;
using MessengerManager.Domain.Interfaces;

namespace MessengerManager.Infrastructure.Data
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T: class
    {
        private readonly MessengerManagerDbContext _dbContext;
        public EfGenericRepository(MessengerManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public T Add(T obj)
        {
            return _dbContext.Set<T>().Add(obj).Entity;
        }

        public T Update(T obj)
        {
            _dbContext.Entry(obj).CurrentValues.SetValues(obj);
            return _dbContext.Set<T>().Update(obj).Entity;
        }
    }
}