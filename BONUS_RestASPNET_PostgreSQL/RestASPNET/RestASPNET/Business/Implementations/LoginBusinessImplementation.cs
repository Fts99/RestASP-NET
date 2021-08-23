using RestASPNET.Configurations;
using RestASPNET.Data.VO;
using RestASPNET.Model;
using RestASPNET.Repository;
using RestASPNET.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestASPNET.Business.Implementations
{
    class LoginBusinessImplementation : ILoginBusiness
    {

        private const string DATE_FORMAT = "yyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredential)
        {
            var user = _repository.ValidateCredentials(userCredential);

            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
            };

            var accesToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpityTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            return SaveUserInfo(user, accesToken, refreshToken);
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accesToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accesToken);
            var userName = principal.Identity.Name;
            var user = _repository.ValidateCredentials(userName);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpityTime <= DateTime.Now)
                return null;

            accesToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            return SaveUserInfo(user, accesToken, refreshToken);
        }

        public TokenVO SaveUserInfo(User user, string accessToken, string refreshToken)
        {
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            _repository.RefreshUserInfo(user);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
