using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Domain.Interfaces
{
    /// <summary>
    /// Interface representing a repository for users, inheriting from the generic repository for entities of type User.
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user to register.</param>
        void Register(User user);

        /// <summary>
        /// Asynchronously performs user login with the provided email and password.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task representing the asynchronous operation that returns the logged-in user, or null if login fails.</returns>
        Task<User> LoginAsync(string email, string password);

        /// <summary>
        /// Asynchronously retrieves a user by email.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation that returns the user, or null if not found.</returns>
        Task<User?> GetByEmailAsync(string email);
    }
}
