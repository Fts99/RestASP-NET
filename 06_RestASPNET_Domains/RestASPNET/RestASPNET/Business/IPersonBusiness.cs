using RestASPNET.Model;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);

        Person FindById(long id);

        List<Person> FindAll();

        Person Update(Person person);

        bool Delete(long id);

    }
}
