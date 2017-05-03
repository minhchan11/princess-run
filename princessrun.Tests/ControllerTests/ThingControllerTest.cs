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
       
        [Fact]
        public async Task Get_Gamepage_Test()
        {
            var contextOptions = new DbContextOptionsBuilder()
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PrincessRun;integrated security=True")
            .Options;
            var _db = new ApplicationDbContext(contextOptions);
            var controller = new HomeController(_userManager, _db);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new MockHttpContext
                {
                    User = new GenericPrincipal(new GenericIdentity("minh"), null)
                }  
            };
            
            var result = await controller.Index();
            Console.WriteLine(result);
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }


        private class MockHttpContext : DefaultHttpContext
        {
            public override ClaimsPrincipal User { get; set; }
        }
    }
}
