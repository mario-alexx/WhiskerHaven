using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Application.Models
{
    /// <summary>
    /// Represents a response object for user login operations.
    /// </summary>
    public class UserLoginResponse
    {
        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the authentication token associated with the user login.
        /// </summary>
        public string Token { get; set; }
    }
}
