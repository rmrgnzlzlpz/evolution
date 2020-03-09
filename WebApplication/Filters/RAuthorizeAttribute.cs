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
using Domain.Entities;

namespace WebApplication.Filters
{
    public class RAuthorizeAttribute : ActionFilterAttribute
    {
        private UnitOfWork _uow;
        private IConfiguration _configuration;
        public UserService _service;

        public RAuthorizeAttribute()
        {
            _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

            string connectionString = _configuration.GetConnectionString("evolution");
            _uow = new UnitOfWork(connectionString);
            _service = new UserService(_uow, _uow.UserRepository);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var id = context.HttpContext.Session.GetString("id") ?? "0";
                var username = context.HttpContext.Session.GetString("username") ?? "";
                var password = context.HttpContext.Session.GetString("password") ?? "";
                var role = context.HttpContext.Session.GetString("role") ?? "0";
                User user = int.Parse(id) < 1 ? null : _service.Find(int.Parse(id));
                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "Login"
                    }));
                } else
                {
                    if (user.Username != username || user.Password != password)
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "User",
                            action = "Login"
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Error"
                }));
                Console.WriteLine(e.Message);
            }
            finally
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
