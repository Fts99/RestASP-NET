using RestASPNET.Data.VO;
using RestASPNET.Model;
using RestASPNET.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestASPNET.Repository
{
    public class UserRepository : IUserRepository   
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pwd = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.User.FirstOrDefault(usr => (usr.UserName == user.UserName) && (usr.Password == pwd));
        }

        public User RefreshUserInfo(User user)
        {
            if (!Exists(user.Id))
            {
                return null;
            }

            var result = _context.User.Find(user.Id);

            try
            {
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        private bool Exists(long id)
        {
            return _context.User.Any(user => user.Id.Equals(id));
        }

        private string ComputeHash(string password, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
