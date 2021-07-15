using RestASPNET.Model;
using System.Collections.Generic;

namespace RestASPNET.Repository
{
    public interface IBookRepository
    {
        Book Create(Book book);

        Book FindById(long id);

        List<Book> FindAll();

        Book Update(Book book);

        bool Delete(long id);

        bool Exists(long id);

    }
}
