using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using princessrun.Controllers;
using princessrun.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace princessrun.Tests.ControllerTests
{
    public class ThingControllerTest
    {
        public ApplicationDbContext _db;
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
    }
}
