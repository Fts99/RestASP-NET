using RestASPNET.Data.VO;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindAll();

        List<PersonVO> FindByName(string firstName, string lastName);

        PersonVO Update(PersonVO person);

        bool Delete(long id);

        PersonVO Disable(long id);

    }
}
