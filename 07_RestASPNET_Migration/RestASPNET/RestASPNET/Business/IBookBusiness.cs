using RestASPNET.Model;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);

        Book FindById(long id);

        List<Book> FindAll();

        Book Update(Book book);

        bool Delete(long id);

        bool ValidateLaunchDate(System.DateTime date);

    }
}
