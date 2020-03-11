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
    public class UserController : Controller
    {
        private List<string> _errors;
        private static IConfiguration _configuration;
        private readonly UnitOfWork _uow;
        private readonly UserService _service;
        private readonly RoleService _roleService;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _uow = new UnitOfWork(configuration.GetConnectionString("evolution"));
            _service = new UserService(_uow, _uow.UserRepository);
            _roleService = new RoleService(_uow, _uow.RoleRepository);
            _errors = new List<string>();
        }

        // GET: User
        [RAuthorize]
        public ActionResult Index()
        {
            PermissionService permissionService = new PermissionService(_uow, _uow.PermissionRepository);
            User user = _service.Find(long.Parse(HttpContext.Session.GetString("id")));
            ViewData["edit"] = permissionService.IsOfRole(user.RoleId, "Editar Usuario");
            return View(user);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(_errors);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            _errors = new List<string>();
            User user = new User { Username = username, Password = password };
            user = _service.GetUser(user);
            if (user == null)
            {
                _errors.Add("Username y/o Password incorrectos.");
                return Login();
            }
            if (!_service.HasPermission(user, "Iniciar Sesion") || user.State != UserState.Active) return RedirectToAction("Unauthorized", "Home");
            HttpContext.Session.SetString("id", user.Id.ToString());
            HttpContext.Session.SetString("role", user.RoleId.ToString());
            HttpContext.Session.SetString("username", user.Username);
            HttpContext.Session.SetString("password", user.Password);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IEnumerable<User> GetAll(int page)
        {
            IEnumerable<User> users = _service.GetAll(page*10 + 1, page*10+11);
            return users;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }

        [Permission("Crear Usuario")]
        public IActionResult Create()
        {
            return View(_roleService.FindBy("state", 1));
        }

        // POST: User/Create
        [HttpPost]
        [Permission("Crear Usuario")]
        public ActionResult Create(User user)
        {
            try
            {
                if (_service.Create(user) != null) {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Error", "Home");
            }
            catch
            {
                _errors.Add("Error al guardar usuario");
                return View(_errors);
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User entity)
        {
            try
            {
                User user = _service.Find(id);
                entity.Username = user.Username;
                entity.RoleId = user.RoleId;
                entity.Id = id;
                _service.Update(entity);
                return RedirectToAction("Login");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}