using RestASPNET.Data.VO;

namespace RestASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO userCredential);

    }
}
