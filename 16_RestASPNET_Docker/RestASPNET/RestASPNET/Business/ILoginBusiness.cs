using RestASPNET.Data.VO;
using RestASPNET.Model;

namespace RestASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO userCredential);

        TokenVO ValidateCredentials(TokenVO token);

        TokenVO SaveUserInfo(User user, string accesToken, string refreshToken);

        bool RevokeToken(string userName);
    }
}
