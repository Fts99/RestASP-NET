using Microsoft.EntityFrameworkCore;
using RestASPNET.Model.Base;
using RestASPNET.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestASPNET.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _mySqlContext;
        private DbSet<T> dataSet;

        public GenericRepository(MySQLContext mySQLContext)
        {
            _mySqlContext = mySQLContext;
            dataSet = _mySqlContext.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataSet.Add(item);
                _mySqlContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }

        public bool Delete(long id)
        {
            if (Exists(id))
            {
                var item = dataSet.Find(id);
                dataSet.Remove(item);
                _mySqlContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<T> FindAll()
        {
            return dataSet.ToList();
        }

        public T FindById(long id)
        {
            return dataSet.Find(id);
        }

        public T Update(T item)
        {
            if (!Exists(item.Id))
            {
                return null;
            }

            try
            {
                dataSet.Update(item);
                _mySqlContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }

        public bool Exists(long id)
        {
            return dataSet.Any(x => x.Id == id);
        }
    }
}
