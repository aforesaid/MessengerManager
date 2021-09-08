using System.Linq;

namespace MessengerManager.Domain.Interfaces
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        T Add(T obj);
        T Update(T obj);
    }
}