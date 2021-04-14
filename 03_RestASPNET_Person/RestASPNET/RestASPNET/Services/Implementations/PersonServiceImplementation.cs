using RestASPNET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestASPNET.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for(int i = 1; i < 8; i++)
            {
                persons.Add(MockPerson(i));
            }
            return persons;
        }

        public Person FindById(long id)
        {
            return MockPerson(id);
        }

        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(long i)
        {
            return new Person
            {
                Id = Interlocked.Increment(ref count),
                FistName = "Fabricio " + count,
                LastName = "Silva " + count,
                Address = "Rua Capitão",
                Gender = "Masculino"
            };
        }
    }
}
