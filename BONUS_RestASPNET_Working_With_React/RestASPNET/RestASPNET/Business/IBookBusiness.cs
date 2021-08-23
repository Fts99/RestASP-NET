using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.Utils;

namespace RestASPNET.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);

        BookVO FindById(long id);

        PagedSearchVO<BookVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int currentPage);

        BookVO Update(BookVO book);

        bool Delete(long id);

        bool ValidateLaunchDate(System.DateTime date);

    }
}
