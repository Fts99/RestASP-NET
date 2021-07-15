using RestASPNET.Model;
using RestASPNET.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestASPNET.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private MySQLContext _mySqlContext;

        public BookRepositoryImplementation(MySQLContext mySQLContext) 
        {
            _mySqlContext = mySQLContext;
        }

        public Book Create(Book book)
        {
            try
            {
                _mySqlContext.Add(book);
                _mySqlContext.SaveChanges();
            }
            catch (Exception )
            {
                throw;
            }
            return book;
        }

        public bool Delete(long id)
        {
            if (Exists(id))
            {
                var book = _mySqlContext.Books.Find(id);
                _mySqlContext.Books.Remove(book);
                _mySqlContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Book> FindAll()
        {
            return _mySqlContext.Books.ToList();
        }

        public Book FindById(long id)
        {
            return _mySqlContext.Books.Find(id);
        }

        public Book Update(Book book)
        {
            if (!Exists(book.Id))
            {
                return null;
            }

            try
            {
                _mySqlContext.Books.Update(book);
                _mySqlContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return book;
        }

        public bool Exists(long id)
        {
            return _mySqlContext.Books.Any(x => x.Id == id);
        }
    }
}
