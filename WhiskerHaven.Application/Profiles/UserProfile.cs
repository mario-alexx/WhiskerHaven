using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Services.UserService.SignIn;
using WhiskerHaven.Application.Services.UserService.SignUp;
using WhiskerHaven.Domain.Entities;

namespace WhiskerHaven.Application.Profiles
{
    /// <summary>
    /// Represents a mapping profile for user-related operations.
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Constructor for the UserProfile class.
        /// </summary>
        public UserProfile()
        {
            // Maps from SignInCommand to User and vice versa.
            CreateMap<SignInCommand, User>().ReverseMap();

            // Maps from SignUpCommand to User and vice versa.
            CreateMap<SignUpCommand, User>().ReverseMap();
        }
    }
}
