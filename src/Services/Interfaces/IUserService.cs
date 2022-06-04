using Meetup.Data.Entities;
using Meetup.Models;

namespace Meetup.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        User GetById(string id);
        Task<AuthenticateResponse> Registation(AuthenticateRequest model);
    }
}
