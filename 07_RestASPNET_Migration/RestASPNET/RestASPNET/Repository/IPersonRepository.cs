using RestASPNET.Model;
using System.Collections.Generic;

namespace RestASPNET.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);

        Person FindById(long id);

        List<Person> FindAll();

        Person Update(Person person);

        bool Delete(long id);

        bool Exists(long id);

    }
}
