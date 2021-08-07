using RestASPNET.Data.Converter.Contract;
using RestASPNET.Data.VO;
using RestASPNET.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestASPNET.Data.Converter.Implementations
{
    public class BookConverter : Iparser<BookVO, Book>, Iparser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null) return null;
            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                price = origin.price,
                title = origin.title
            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null) return null;
            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                price = origin.price,
                title = origin.title
            };
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
