using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Services;
using Domain.Entities;
using Infraestructura.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication.Filters;

namespace WebApplication.Controllers
{
    public class Rolecontroller : Controller
    {
        private static IConfiguration _configuration;
        private readonly UnitOfWork _uow;
        private readonly RoleService _service;
        public Rolecontroller(IConfiguration configuration)
        {
            _configuration = configuration;
            _uow = new UnitOfWork();
            _service = new RoleService(_uow, _uow.RoleRepository);
        }
        
        [RAuthorize]
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }
    }
}