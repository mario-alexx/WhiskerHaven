using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Application.Models;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Domain.Interfaces;

namespace WhiskerHaven.Application.Services.JwtService
{
    /// <summary>
    /// Represents a service for JWT token generation.
    /// </summary>
    public class JwtService : IJwtService
    {
        /// <summary>
        /// The configuration instance for accessing app settings.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The repository for user-related operations.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor for the JwtService class.
        /// </summary>
        /// <param name="configuration">The configuration instance for accessing app settings.</param>
        /// <param name="userRepository">The repository for user-related operations.</param>
        public JwtService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        //public async Task<UserLoginResponse> GenerateAsync(User user)
        //{
        //    string key = _configuration["Jwt:Key"];
        //    string issuer = _configuration["Jwt:Issuer"];
        //    string audience = _configuration["Jwt:Audience"];

        //    var userEntity = await  _userRepository.GetByEmailAsync(user.Email);

        //    var claims = new Claim[]
        //    {
        //        new(JwtRegisteredClaimNames.Name, userEntity.Name),
        //        new(JwtRegisteredClaimNames.FamilyName, userEntity.LastName),
        //        new(JwtRegisteredClaimNames.Email, user.Email)
        //    };

        //    //SecurityTokenDescriptor tokenDescriptor =

        //    var signinCredentials = new SigningCredentials(
        //                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        //                            SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer,
        //        audience,
        //        claims,
        //        null,
        //        DateTime.UtcNow.AddHours(1),
        //        signinCredentials);


        //    // return usuarioLoginRespuestaDto;

        //    var tokenValue = new JwtSecurityTokenHandler();


        //    var usuerLoginResponse = new UserLoginResponse()
        //    {
        //        Token = tokenValue.WriteToken(token),
        //        User = userEntity
        //    };

        //    return usuerLoginResponse;
        //}

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the JWT token is generated.</param>
        /// <returns>The generated JWT token and user login response.</returns>
        public UserLoginResponse Generate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature),
                //Issuer = _configuration["Jwt:Issuer"],
                //Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDesc);
            var userloginResponse = new UserLoginResponse()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };

            return userloginResponse;
        }
    }
}
