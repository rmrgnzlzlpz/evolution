using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Services;
using Domain.Entities;
using Infraestructura.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApplication.Filters;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private static IConfiguration _configuration;
        private readonly UnitOfWork _uow;
        private readonly UserService _userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _uow = new UnitOfWork(configuration.GetConnectionString("evolution"));
            _userService = new UserService(_uow, _uow.UserRepository);
            _logger = logger;
        }

        [RAuthorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scene()
        {
            return View();
        }

        public IActionResult Render()
        {
            return View(_userService.GetAll());
        }

        [RAuthorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public new IActionResult Unauthorized()
        {
            return View();
        }
    }
}
