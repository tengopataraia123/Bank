using Bank.DataBase;
using Bank.DataBase.Models;
using Bank.DTOs;
using Bank.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services
{
    public interface IUserService
    {
        public Task RegisterUser(UserDTO user);
        public Task<UserDTO> GetUserById(int id);
    }
    public class UserService : IUserService
    {
        private readonly BankDbContext context;
        public UserService(BankDbContext context)
        {
            this.context = context;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(o => o.Id == id);
            if (user == null)
                throw new UserDoesNotExist();
            return new UserDTO
            {
                Age = user.Age,
                FirstName = user.FirstName,
                Income = user.Income,
                LastName = user.LastName,
                Mail = user.Mail,
                Username = user.Username
            };
        }

        public async Task RegisterUser(UserDTO user)
        {
            if (await context.Users.AnyAsync(o => o.Username == user.Username))
                throw new UserAlreadyExistsException();
            context.Add(new User
            {
                Age = user.Age,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Income = user.Income,
                Mail = user.Mail,
                Username = user.Username
            });

            await context.SaveChangesAsync();
        }
    }
}
