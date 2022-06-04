using Meetup.DAO.Interfaces;
using Meetup.Data.Entities;
using Meetup.Models;
using Meetup.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Meetup.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly SignInManager<User> _signInManager;
        public UserService(IOptions<AppSettings> appSettings, SignInManager<User> signInManager)
        {
            _appSettings = appSettings.Value;
            _signInManager = signInManager;
        }
        public async Task<AuthenticateResponse> Registation(AuthenticateRequest model)
        {
            var user = new User() { UserName = model.Username };
            var resut = await _signInManager.UserManager.CreateAsync(user, model.Password);

            if (resut.Succeeded)
            {
                var token = generateJwtToken(user);
                return new AuthenticateResponse(user, token);
            }
            throw new Exception();
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = _signInManager.UserManager.Users.FirstOrDefault(x => x.UserName == model.Username);
            if (user == null) return null;
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (!result.Succeeded)
                return null;
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        public User GetById(string id)
        {
            return _signInManager.UserManager.Users.FirstOrDefault(x => x.Id == id);
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}