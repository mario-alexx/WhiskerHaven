using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;
using WhiskerHaven.Infrastructure.Data;
using XSystem.Security.Cryptography;

namespace WhiskerHaven.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a repository implementation for users, inheriting from the generic repository for entities of type User.
    /// </summary>
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        /// <summary>
        /// Constructor for the UserRepository class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Asynchronously retrieves a user by email.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the user, or null if not found.</returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (userExist == null)
            {
                return null;
            }
            return userExist;
        }

        /// <summary>
        /// Asynchronously performs user login with the provided email and password.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task representing the asynchronous operation that returns the logged-in user, or null if login fails.</returns>
        public async Task<User> LoginAsync(string email, string password)
        {
            var passCryp = getMd5(password);
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == passCryp);

            if (user == null)
            {
                return null;
            }
            return user;   
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user to register.</param>
        public void Register(User user)
        {
            var newPassword = getMd5(user.Password);
            _context.Users.Add(user);
            user.Password = newPassword;
        }


        /// <summary>
        /// Encrypts the given password using MD5 hashing algorithm.
        /// </summary>
        /// <param name="password">The password to encrypt.</param>
        /// <returns>The hashed password.</returns>
        public static string getMd5(string password)
        {
            MD5CryptoServiceProvider x = new();
            byte[] data = Encoding.UTF8.GetBytes(password);
            data = x.ComputeHash(data);
            string response = "";
            for (int i = 0; i < data.Length; i++)
                response += data[i].ToString("x2".ToLower());

            return response;
        }
    }
}
