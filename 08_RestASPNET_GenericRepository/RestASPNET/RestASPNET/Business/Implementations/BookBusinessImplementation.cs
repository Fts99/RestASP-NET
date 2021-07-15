using RestASPNET.Model;
using RestASPNET.Repository;
using System;
using System.Collections.Generic;

namespace RestASPNET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusinessImplementation(IRepository<Book> repository) 
        {
            _repository = repository;
        }

        public Book Create(Book book)
        {
            book.LaunchDate = TimeZoneInfo.ConvertTimeToUtc(book.LaunchDate);
            return ValidateLaunchDate(book.LaunchDate) ? _repository.Create(book) : null;
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Update(Book book)
        {
            book.LaunchDate = TimeZoneInfo.ConvertTimeToUtc(book.LaunchDate);
            return ValidateLaunchDate(book.LaunchDate) ? _repository.Update(book) : null;
        }

        public bool ValidateLaunchDate(DateTime launchDate)
        {

            DateTime today = DateTime.UtcNow;
            Console.WriteLine(launchDate);
            Console.WriteLine(launchDate.Subtract(today));

            if (launchDate.Subtract(today).Days >= 1)
            {
                return false;
            }

            return true;

        }

    }
}
