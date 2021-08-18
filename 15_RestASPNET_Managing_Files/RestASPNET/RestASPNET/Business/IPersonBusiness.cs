using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindAll();

        List<PersonVO> FindByName(string firstName, string lastName);

        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int currentPage);

        PersonVO Update(PersonVO person);

        bool Delete(long id);

        PersonVO Disable(long id);

    }
}
