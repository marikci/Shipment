using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Shipment.Core
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private ConcurrentDictionary<int, T> _dic = new ConcurrentDictionary<int, T>();
        public InMemoryRepository()
        {
        }

        public List<T> GetAll()
        {
            return _dic.Values.ToList();
        }

        public T Get(int id)
        {
            T value;
            if (_dic.TryGetValue(id, out value))
            {
                return value;
            }
            else
            {
                throw new Exception($"Data not found, id:{id}");
            }
        }

        public void Upsert(int id, T t)
        {
            if (!_dic.ContainsKey(id))
            {
                _dic.TryAdd(id, t);
            }
            else
            {
                _dic.TryUpdate(id, t, Get(id));
            }
        }

        public void Delete(int id)
        {
            _dic.TryRemove(id, out _);
        }

        public int GetMaxKey()
        {
            return _dic.Keys.Any() ? _dic.Keys.Max() : 0;
        }

    }
}