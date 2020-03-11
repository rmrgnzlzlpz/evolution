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
        private PermissionService _service;
        public string Permission { get; set; }

        public PermissionAttribute(string permission)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            string connectionString = _configuration.GetConnectionString("evolution");
            _uow = new UnitOfWork(connectionString);
            _service = new PermissionService(_uow, _uow.PermissionRepository);
            Permission = permission;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            try
            {
                var role = context.HttpContext.Session.GetString("role") ?? "0";
                if (Permission.Length > 0)
                {
                    if (!_service.IsOfRole(int.Parse(role), Permission))
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Unauthorized"
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Error",
                    model = e.Message
                }));
                Console.WriteLine(e.Message);
            }
        }
    }
}
