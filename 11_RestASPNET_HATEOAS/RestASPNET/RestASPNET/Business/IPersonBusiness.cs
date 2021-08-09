using RestASPNET.Data.VO;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindAll();

        PersonVO Update(PersonVO person);

        bool Delete(long id);

    }
}
