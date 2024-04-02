using Castle.Core.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhiskerHaven.API.Controllers;
using WhiskerHaven.Application.Services.UserService.SignUp;

namespace WhiskerHaven.Test.API.Controllers
{
    public class AccountControllerTests
    {
        //[Fact]
        //public async Task When_CreateUser_Expect_ReturnOk()
        //{
        //    // Arrange
        //    var mediatorMock = new Mock<IMediator>();
        //    var loggerMock = new Mock<ILogger<AccountController>>();
        //    var controller = new AccountController(mediatorMock.Object, loggerMock.Object);

        //    string name = "Pepe";
        //    string lastName = "Castro";
        //    string email = "pepe@email.com";
        //    string password = "Pepecastro123$";
        //    string phoneNumber = "+4328756789";
        //    var signUpCommand = new SignUpCommand(name, lastName, email, password, phoneNumber);


        //    // Act
        //    var result = await controller.Create(signUpCommand) as ObjectResult;

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        //}
    }
}
