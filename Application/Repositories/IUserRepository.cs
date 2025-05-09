using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOModels;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto?> GetUserAsync(string username,  string password);
    }
}
