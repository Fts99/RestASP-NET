using RestASPNET.Data.Converter.Implementations;
using RestASPNET.Data.VO;
using RestASPNET.Model;
using RestASPNET.Repository;
using System.Collections.Generic;

namespace RestASPNET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository) 
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            System.Console.WriteLine(personEntity.FirstName);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public bool Delete(long id)
        {
            return _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

    }
}
