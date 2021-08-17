using RestASPNET.Model;
using RestASPNET.Model.Context;
using RestASPNET.Repository.Generic;
using System;

namespace RestASPNET.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context){}

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
    }

}
