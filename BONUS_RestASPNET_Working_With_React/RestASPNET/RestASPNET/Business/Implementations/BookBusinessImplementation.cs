using RestASPNET.Data.Converter.Implementations;
using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.Utils;
using RestASPNET.Model;
using RestASPNET.Repository;
using System;

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

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int currentPage)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = (pageSize < 1) ? 1 : pageSize;
            var offset = currentPage > 0 ? (currentPage - 1) * size : 0;

            string query = @"select *from books p ";
            if (!string.IsNullOrWhiteSpace(title))
            {
                query += $"where p.title like '%{title}%' or p.author like '%{title}%'";
            }
            query += $" order by p.title {sort} limit {size} offset {offset}";

            int orderPosition = query.IndexOf("order by") + "order by".Length;
            int queryLength = query.Length + "order by".Length;

            string countQuery = query.Replace("*", "count(1) ");
            countQuery = countQuery.Remove(orderPosition, queryLength - orderPosition);

            var persons = _repository.FindWithPagedSearch(query);
            int totalResult = _repository.GetCount(countQuery);

            return new PagedSearchVO<BookVO>()
            {
                CurrentPage = currentPage,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sortDirection,
                TotalResuls = totalResult
            };
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
