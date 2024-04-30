using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TEST_API.Databases;
using TEST_API.Models.JWT;

namespace TEST_API.Helpers.JWT
{
    public class JwtAuthManagerHelpers
    {
        private readonly IConfigurationRoot _configuration;

        public JwtAuthManagerHelpers()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public JwtAuthResponse? Authenticate(string userName, string password)
        {
            var db = new FakeDatabase().GetDb();

            var user = db.Where(x => x.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            if (user.Password != password)
            {
                return null;
            }

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(int.Parse(_configuration.GetSection("JWT_TOKEN_VALIDITY_MINS").Value!));
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT_SECURITY_KEY").Value!);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new JwtAuthResponse
            {
                Token = token,
                Email = user.Email,
                UserName = user.UserName,
                Expires_In = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
        }
    }
}
