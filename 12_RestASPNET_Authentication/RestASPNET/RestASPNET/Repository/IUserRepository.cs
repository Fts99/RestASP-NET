using RestASPNET.Model;
using RestASPNET.Data.VO;

namespace RestASPNET.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
    }
}
