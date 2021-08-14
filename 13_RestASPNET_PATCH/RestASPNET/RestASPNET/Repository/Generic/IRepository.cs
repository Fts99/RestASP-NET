using RestASPNET.Model.Base;
using System.Collections.Generic;

namespace RestASPNET.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);

        T FindById(long id);

        List<T> FindAll();

        T Update(T item);

        bool Delete(long id);

        bool Exists(long id);

    }
}
