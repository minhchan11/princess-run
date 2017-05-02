using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using princessrun.Controllers;
using princessrun.Models;
using princessrun.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using System.Threading;
using Moq;

namespace princessrun.Tests.ControllerTests
{
    public class ThingControllerTest
    {
        public UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> _signInManager;

        [Fact]
        public async Task Get_ViewResult_Index_Test()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PrincessRun;integrated security=True")
            .Options;
            var _db = new ApplicationDbContext(contextOptions);
            var controller = new ThingsController(_db);
            //Act
            var result = await controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Get_ModelList_Index_Test()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder()
           .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PrincessRun;integrated security=True")
           .Options;
            var _db = new ApplicationDbContext(contextOptions);
            var controller = new ThingsController(_db);
            IActionResult actionResult = await controller.Index();
            ViewResult indexView = await controller.Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsType<List<Thing>>(result);
        }

        //[Fact]
        //public async Task Get_Gamepage_Test()
        //{
        //    var contextOptions = new DbContextOptionsBuilder()
        //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PrincessRun;integrated security=True")
        //    .Options;
        //    var _db = new ApplicationDbContext(contextOptions);
        //    var controller = new AccountController(_userManager, _signInManager, _db);

        //    var controllerContext = new Mock<ControllerContext>();
        //    var principal = new Moq.Mock<ClaimsPrincipal>();
        //    principal.SetupGet(x => x.Identity.Name).Returns("minh");
        //    controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
        //    controller.ControllerContext = controllerContext.Object;
        //    var homeController = new HomeController(_userManager, _db);
        //    var result = homeController.Index();
        //    Assert.IsType<ViewResult>(result);

        //    //var mock = new Mock<ControllerContext>();
        //    //mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("SOMEUSER");
        //    //mock.SetupGet(x => x.HttpContext.User.Identity.IsAuthenticated).Returns(true);
        //    //controller.ControllerContext = mock.Object;
        //    //var homeController = new HomeController(_userManager, _db);
        //    //LoginViewModel model = new LoginViewModel();
        //    //model.Email = "minh";
        //    //model.Password = "Cupcake12#";
        //    //var result = await controller.Login(model) as ViewResult;

        //    //var expected = await homeController.Index() as ViewResult;
        //    //Assert.Equal(result, expected);
        //}
       
        [Fact]
        public async Task Get_Gamepage_Test()
        {
            var contextOptions = new DbContextOptionsBuilder()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PrincessRun;integrated security=True")
            .Options;
            var _db = new ApplicationDbContext(contextOptions);
            var controller = new HomeController(_userManager, _db);

            var principal = new GenericPrincipal(new GenericIdentity("minh"), null);
            //var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            //    {
            //    new Claim(ClaimTypes.NameIdentifier, "minh")
            //            }));
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("SOMEUSER");
            mock.SetupGet(x => x.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext = mock.Object;
            var result = await controller.Index();
            Console.WriteLine(result);
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        //var claims = new Claim[]
        //        {
        //             new Claim(ClaimTypes.NameIdentifier, "1"),
        //             new Claim(ClaimTypes.Name, "minh")
        //        };
        //var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
        //var genericIdentity = new GenericIdentity("");
        //genericIdentity.AddClaims(claims);
        //    var genericPrincipal = new GenericPrincipal(genericIdentity, new string[] { });
        //[Fact]
        //public async Task Get_Gamepage_Test2()
        //{
        //    var contextOptions = new DbContextOptionsBuilder()
        //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PrincessRun;integrated security=True")
        //    .Options;
        //    var _db = new ApplicationDbContext(contextOptions);
        //    var controller = new AccountController(_userManager, _signInManager, _db);
        //    //var controller = new HomeController(_userManager, _db);
        //    const string userEmail = "some-email@example.com";
        //    var controllerContext = new TestableControllerContext();
        //    var principal = new GenericPrincipal(new GenericIdentity(userEmail), null);
        //    var testableHttpContext = new TestableHttpContext
        //    {
        //        User = principal
        //    };
        //    controllerContext.HttpContext = testableHttpContext;
        //    controller.ControllerContext = controllerContext;

        //    var result = controller.Index();
        //    Console.WriteLine(result);

        //    Assert.IsType<ViewResult>(result);


        //}

        //[Fact]
        //public async Task Get_Gamepage_Test()
        //{
        //    // Arrange
        //    var validPrincipal = new ClaimsPrincipal(
        //        new[]
        //        {
        //    new ClaimsIdentity(
        //        new[] {new Claim(ClaimTypes.NameIdentifier, "MyUserId")})
        //        });

        //    var httpContext = Substitute.For<HttpContext>();
        //    httpContext.User.Returns(validPrincipal);

        //    var contextOptions = new DbContextOptionsBuilder()
        //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PrincessRun;integrated security=True")
        //    .Options;
        //    var _db = new ApplicationDbContext(contextOptions);
        //    var controller = new AccountController(_userManager, _signInManager, _db) {
        //        ActionContext = new ActionContext
        //        {
        //            HttpContext = httpContext
        //        }
        //    };
        //    var homeController = new HomeController(_userManager, _db);
        //    LoginViewModel model = new LoginViewModel();
        //    model.Email = "minh";
        //    model.Password = "Cupcake12#";
        //    var result = await controller.Login(model) as ViewResult;

        //    var expected = await homeController.Index() as ViewResult;
        //    Assert.Equal(result, expected);
        //}
    }
}
