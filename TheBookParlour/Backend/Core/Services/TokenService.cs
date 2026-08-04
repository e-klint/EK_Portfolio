using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheBookParlour.Core.Interfaces;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Core.Services
{
    public class TokenService: ITokenService
    {   
        //Detta fält och konstruktor behövs om man använder Azure Key Vault
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            //Sätta upp kryptering. Samma säkerhetsnyckel som när vi satte upp tjänsten
            //Denna förvaras på ett säkert ställe tex Azure Keyvault eller liknande och hårdkodas
            //inte in på detta sätt.
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsAVerySecretJwtKey_ForBookshop!"));

            // Hämta nyckeln från Key Vault via IConfiguration
            var jwtKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            //Lista med vilka behörigheter som en användare har
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
              new Claim(ClaimTypes.Name, user.UserName),
              new Claim(ClaimTypes.Role, user.Role)
             };

            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Skapa options för att sätta upp en token
            var tokenOptions = new JwtSecurityToken(
                    issuer: "http://BookShop",
                    audience: "http://BookShop",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signinCredentials);

            //Generar en ny token som skall skickas tillbaka 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

    }
}
