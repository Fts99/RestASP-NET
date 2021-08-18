using RestASPNET.Data.Converter.Implementations;
using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.Utils;
using RestASPNET.Model;
using RestASPNET.Repository;
using System;
using System.Collections.Generic;

namespace RestASPNET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository) 
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int currentPage)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = (pageSize < 1) ? 1 : pageSize;
            var offset = currentPage > 0 ? (currentPage - 1) * size : 0;

            string query = @"select *from person p ";
            if (!string.IsNullOrWhiteSpace(name)) {
                query += $"where p.first_name like '%{name}%' or p.last_name like '%{name}%'";
            } 
            query += $" order by p.first_name {sort} limit {size} offset {offset}";
            
            int orderPosition = query.IndexOf("order by") + "order by".Length;
            int queryLength = query.Length + "order by".Length;

            string countQuery = query.Replace("*", "count(1) ");
            countQuery = countQuery.Remove(orderPosition, queryLength - orderPosition);

            var persons = _repository.FindWithPagedSearch(query);
            int totalResult = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>()
            {
                CurrentPage = currentPage,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sortDirection,
                TotalResuls = totalResult
            };
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

    }
}
