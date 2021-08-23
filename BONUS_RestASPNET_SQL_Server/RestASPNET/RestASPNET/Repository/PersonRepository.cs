using RestASPNET.Model;
using RestASPNET.Model.Context;
using RestASPNET.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestASPNET.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(SQLServerContext context) : base(context){}

        public Person Disable(long id)
        {
            if (!Exists(id))
                return null;

            var user = FindById(id);

            if(user is not null)
            {
                user.Enabled = false;
                try
                {
                    _mySqlContext.Entry(user).CurrentValues.SetValues(user);
                    _mySqlContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return user;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            var result = _mySqlContext.Persons.Where(p =>
                (!string.IsNullOrWhiteSpace(firstName + lastName) &&
                p.FirstName.IndexOf(firstName,StringComparison.OrdinalIgnoreCase) >= 0 && 
                p.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0) || 
                (!string.IsNullOrWhiteSpace(firstName) &&
                string.IsNullOrWhiteSpace(lastName) &&
                p.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (!string.IsNullOrWhiteSpace(lastName) &&
                string.IsNullOrWhiteSpace(firstName) &&
                p.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();

            return result.Count == 0 ? null : result;
        }
    }

}
