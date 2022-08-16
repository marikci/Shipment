using System.Collections.Generic;

namespace Shipment.Core
{
    public interface IRepository<T> where T: class
    {
        List<T> GetAll();
        T Get(int id);
        void Upsert(int id, T t);
        void Delete(int id);
        int GetMaxKey();
    }
}
