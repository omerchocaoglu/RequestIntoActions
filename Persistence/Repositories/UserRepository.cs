using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.DTOModels;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> GetUserAsync(string username, string password)
        {
            //var user = await _context.Users
            //    .Where(x => x.Email == email && x.Password == password) // Gerçek projede Password hash olmalı!
            //    .Select(x => new UserDto
            //    {
            //        ID = x.ID,
            //        Email = x.Email,
            //        FullName = $"{x.Name} {x.SurName}"
            //    })
            //    .FirstOrDefaultAsync();

            //return user;
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower() && u.Password == password);

            if (user == null)
                return null;

            return new UserDto
            {
                ID = user.ID,
                Email = user.Email,
                FullName = user.Name + " " + user.SurName,
                UserName = user.Name,
                // diğer gerekli alanlar
            };
        }
    }

}
