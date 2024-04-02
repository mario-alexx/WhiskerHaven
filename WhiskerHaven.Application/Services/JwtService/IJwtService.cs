using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Application.Services.JwtService
{
    /// <summary>
    /// Represents a service for JWT token generation.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the JWT token is generated.</param>
        /// <returns>The generated JWT token.</returns>
        UserLoginResponse Generate(User user);
    }
}
