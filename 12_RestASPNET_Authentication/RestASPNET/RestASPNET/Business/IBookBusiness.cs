using RestASPNET.Data.VO;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);

        BookVO FindById(long id);

        List<BookVO> FindAll();

        BookVO Update(BookVO book);

        bool Delete(long id);

        bool ValidateLaunchDate(System.DateTime date);

    }
}
