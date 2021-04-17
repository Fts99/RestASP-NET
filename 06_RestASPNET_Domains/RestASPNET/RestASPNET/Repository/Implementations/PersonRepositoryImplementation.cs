using RestASPNET.Model;
using RestASPNET.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestASPNET.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySQLContext _mySqlContext;

        public PersonRepositoryImplementation(MySQLContext mySQLContext) 
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

        public bool Delete(long id)
        {
            if (Exists(id))
            {
                var person = _mySqlContext.Persons.Find(id);
                _mySqlContext.Persons.Remove(person);
                _mySqlContext.SaveChanges();
                return true;
            }
            return false;
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
            if (!Exists(person.Id))
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

        public bool Exists(long id)
        {
            return _mySqlContext.Persons.Any(x => x.Id == id);
        }
    }
}
