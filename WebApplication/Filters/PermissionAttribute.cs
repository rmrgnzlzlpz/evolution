using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Aplicacion.Services;
using Microsoft.Extensions.Configuration;
using Infraestructura.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.IO;

namespace WebApplication.Filters
{
    public class PermissionAttribute : ActionFilterAttribute
    {
        private UnitOfWork _uow;
        private IConfiguration _configuration;
        public PermissionService _service;
        public string Permission { get; set; }

        public PermissionAttribute()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            string connectionString = _configuration.GetConnectionString("evolution");
            _uow = new UnitOfWork(connectionString);

            _service = new PermissionService(_uow, _uow.PermissionRepository);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var identity = (ClaimsIdentity)context.HttpContext.User.Identity;
            var role = identity.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;
            try
            {
                if (Permission.Length > 0)
                {
                    if (!_service.IsOfRole(int.Parse(role), Permission))
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Index"
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
