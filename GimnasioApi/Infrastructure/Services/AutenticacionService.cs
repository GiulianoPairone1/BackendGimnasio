using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class AutenticacionService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AutenticacionServiceOptions _options;

        public AutenticacionService(IUserRepository userRepository, IOptions<AutenticacionServiceOptions> options)
        {
            _userRepository = userRepository;
            _options= options.Value;
        }

        private User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var user = _userRepository.GetUserByMail(authenticationRequest.Email);

            if (user == null) return null;

            if (authenticationRequest.UserType == typeof(SuperAdmin).Name || authenticationRequest.UserType == typeof(Admin).Name || authenticationRequest.UserType == typeof(Trainer).Name || authenticationRequest.UserType == typeof(Client).Name)
            {
                if (user.Password == authenticationRequest.Password) return user;
            }

            return null;
        }
        public string Autenticar(AuthenticationRequest authenticationRequest)
        {
            // Validar credenciales
            var user = ValidateUser(authenticationRequest);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User authentication failed");
            }

            //
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_email", user.Email));
            claimsForToken.Add(new Claim("usertype", authenticationRequest.UserType));

            var jwtSecurityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials);

            var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
                .WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();

        }
        
        public class AutenticacionServiceOptions
        {
            public const string AutenticacionService = "AutenticacionService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
