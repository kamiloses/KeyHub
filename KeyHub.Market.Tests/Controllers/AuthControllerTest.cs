// using System.Threading.Tasks;
// using JetBrains.Annotations;
// using KeyHub.Market.Controllers;
// using KeyHub.Market.Models;
// using KeyHub.Market.Models.ViewModels;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Xunit;
//
// namespace KeyHub.Market.Tests.Controllers;
//
// [TestSubject(typeof(AuthController))]
// public class AuthControllerTest
// {
//     private readonly Mock<UserManager<User>> _userManagerMock;
//     private readonly Mock<SignInManager<User>> _signInManagerMock;
//     private readonly AuthController _controller;
//
//     public AuthControllerTest()
//     {
//         var userStoreMock = new Mock<IUserStore<User>>();
//         _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
//
//         var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
//         var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
//         _signInManagerMock = new Mock<SignInManager<User>>(
//             _userManagerMock.Object,
//             contextAccessorMock.Object,
//             claimsFactoryMock.Object,
//             null, null, null, null
//         );
//
//         _controller = new AuthController(_signInManagerMock.Object, _userManagerMock.Object);
//     }
//
//     [Fact]
//     public void Login_Get_ReturnsView()
//     {
//         var result = _controller.Login();
//         Assert.IsType<ViewResult>(result);
//     }
//
//     [Fact]
//     public async Task Login_Post_ValidModel_SuccessfulLogin_RedirectsHome()
//     {
//         var model = new LoginViewModel { UserName = "testuser", Password = "Password123", RememberMe = false };
//         _signInManagerMock.Setup(s => s.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false))
//             .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
//
//         var result = await _controller.Login(model);
//
//         var redirect = Assert.IsType<RedirectToActionResult>(result);
//         Assert.Equal("Home", redirect.ActionName);
//         Assert.Equal("Home", redirect.ControllerName);
//     }
//
//     [Fact]
//     public async Task Login_Post_InvalidLogin_ReturnsViewWithError()
//     {
//         var model = new LoginViewModel { UserName = "testuser", Password = "wrong", RememberMe = false };
//         _signInManagerMock.Setup(s => s.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false))
//             .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
//
//         var result = await _controller.Login(model);
//
//         var viewResult = Assert.IsType<ViewResult>(result);
//         Assert.Equal(model, viewResult.Model);
//         Assert.False(_controller.ModelState.IsValid);
//     }
//
//     [Fact]
//     public async Task Logout_Post_RedirectsHome()
//     {
//         var result = await _controller.Logout();
//
//         _signInManagerMock.Verify(s => s.SignOutAsync(), Times.Once);
//         var redirect = Assert.IsType<RedirectToActionResult>(result);
//         Assert.Equal("Home", redirect.ActionName);
//         Assert.Equal("Home", redirect.ControllerName);
//     }
//
//     [Fact]
//     public void Register_Get_ReturnsView()
//     {
//         var result = _controller.Register();
//         Assert.IsType<ViewResult>(result);
//     }
//
//     [Fact]
//     public async Task Register_Post_ValidModel_SuccessfulRegistration_RedirectsHome()
//     {
//         var model = new RegisterViewModel { UserName = "newuser", Email = "test@test.com", Password = "Password123" };
//         _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<User>(), model.Password))
//             .ReturnsAsync(IdentityResult.Success);
//         _signInManagerMock.Setup(s => s.SignInAsync(It.IsAny<User>(), false, null))
//             .Returns(Task.CompletedTask);
//
//         var result = await _controller.Register(model);
//
//         var redirect = Assert.IsType<RedirectToActionResult>(result);
//         Assert.Equal("Home", redirect.ActionName);
//         Assert.Equal("Home", redirect.ControllerName);
//     }
//
//     [Fact]
//     public async Task Register_Post_FailedRegistration_ReturnsViewWithErrors()
//     {
//         var model = new RegisterViewModel { UserName = "newuser", Email = "test@test.com", Password = "Password123" };
//         _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<User>(), model.Password))
//             .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Email already exists" }));
//
//         var result = await _controller.Register(model);
//
//         var viewResult = Assert.IsType<ViewResult>(result);
//         Assert.Equal(model, viewResult.Model);
//         Assert.False(_controller.ModelState.IsValid);
//         Assert.Contains("Email", _controller.ModelState.Keys);
//     }
// }
