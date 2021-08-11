using RestASPNET.Data.Converter.Implementations;
using RestASPNET.Data.VO;
using RestASPNET.Model;
using RestASPNET.Repository;
using System;
using System.Collections.Generic;

namespace RestASPNET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository) 
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            //book.LaunchDate = TimeZoneInfo.ConvertTimeToUtc(book.LaunchDate);
            var bookEntity = _converter.Parse(book);
            return ValidateLaunchDate(bookEntity.LaunchDate) ? _converter.Parse(_repository.Create(bookEntity)) : null;
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            //book.LaunchDate = TimeZoneInfo.ConvertTimeToUtc(book.LaunchDate);
            var bookEntity = _converter.Parse(book);
            return ValidateLaunchDate(bookEntity.LaunchDate) ? _converter.Parse(_repository.Update(bookEntity)) : null;
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
