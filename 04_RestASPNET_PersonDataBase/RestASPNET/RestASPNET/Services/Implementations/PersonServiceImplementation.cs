using RestASPNET.Model;
using RestASPNET.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestASPNET.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private MySQLContext _mySqlContext;

        public PersonServiceImplementation(MySQLContext mySQLContext) 
        {
            _mySqlContext = mySQLContext;
        }

        public Person Create(Person person)
        {
            try
            {
                _mySqlContext.Add(person);
                _mySqlContext.SaveChanges();
            }
            catch (Exception )
            {
                throw;
            }
            return person;
        }

        public void Delete(long id)
        {
            if (_mySqlContext.Persons.Find(id) != null)
            {
                var person = _mySqlContext.Persons.Find(id);
                _mySqlContext.Persons.Remove(person);
                _mySqlContext.SaveChanges();
            }
        }

        public List<Person> FindAll()
        {
            return _mySqlContext.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _mySqlContext.Persons.Find(id);
        }

        public Person Update(Person person)
        {
            if (!_mySqlContext.Persons.Contains(person))
            {
                return null;
            }

            try
            {
                _mySqlContext.Persons.Update(person);
                _mySqlContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

    }
}
