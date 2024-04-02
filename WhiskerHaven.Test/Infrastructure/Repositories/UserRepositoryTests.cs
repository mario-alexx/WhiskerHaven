using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.Domain.Entities;
using WhiskerHaven.Infrastructure.Data;
using WhiskerHaven.Infrastructure.Repositories;

namespace WhiskerHaven.Test.Infrastructure.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task When_GetByEmailAsync_UserExists_Expect_ReturnUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var email = "mario@email.com";
            var password = "Mario123$";
            var name = "Mario";
            var lastName = "Toro";
            var phoneNumber = "+123233242";
            var sut = new User { Email = email, Password = password, Name = name, LastName = lastName, PhoneNumber = phoneNumber };

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                context.Users.Add(sut);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                var repository = new UserRepository(context);

                // Act
                var result = await repository.GetByEmailAsync(email);

                // Assert 
                Assert.NotNull(result);
                Assert.Equal(email, result.Email);
            }
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var email = "mario@email.com";
            var password = "Mario123$";
            var name = "Mario";
            var lastName = "Toro";
            var phoneNumber = "+123233242";
            var encryptedPassword = UserRepository.getMd5(password);

            var sut = new User { Email = email, Password = encryptedPassword, Name = name, LastName = lastName, PhoneNumber = phoneNumber };

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                context.Users.Add(sut);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                var repository = new UserRepository(context);

                // Act
                var result = await repository.LoginAsync(email, password);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(email, result.Email);
            }
        }

        [Fact]
        public void When_RegisterUser_Expect_AddsUserToContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var email = "mario@email.com";
            var password = "Mario123$";
            var name = "Mario";
            var lastName = "Toro";
            var phoneNumber = "+123233242";

            var sut = new User { Email = email, Password = password, Name = name, LastName = lastName, PhoneNumber = phoneNumber };

            using (var context = new ApplicationDbContext(options, Mock.Of<ILogger<ApplicationDbContext>>()))
            {
                var repository = new UserRepository(context);

                // Act
                repository.Register(sut);

                // Assert
                Assert.Contains(sut, context.Users.Local);
            }
        }
    }
}
