using Infraestructura.Interfaces;
using Infraestructura.Utils;
using NUnit.Framework;
using Domain.Entities;
using Aplicacion.Services;
using System.Collections.Generic;

namespace TestContext
{
    public class Tests
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Role role = new Role
            {
                Name = "Rol de Prueba 2",
                Description = "Descripción de Rol de Prueba 2",
                State = RoleState.Active
            };
            unitOfWork.Init();
            Role savedRole = unitOfWork.RoleRepository.Add(role);
            unitOfWork.Save();
            Assert.AreEqual(savedRole, role);
        }

        [Test]
        public void TestUserAd()
        {
            UserService userService = new UserService(unitOfWork, unitOfWork.UserRepository);
            User user = new User
            {
                Username = "rmrgnzlz",
                Password = "1234",
                Firstname = "Ramiro",
                Lastname = "Gonzalez",
                RoleId = 1,
                State = UserState.Active
            };
            User savedUser = userService.Create(user);
            Assert.IsNotNull(savedUser);
        }

        [Test]
        public void TestUserFind()
        {
            long id = 1;
            User user = unitOfWork.UserRepository.Find(id);
            Assert.IsNotNull(user);
        }

        [Test]
        public void TestUserFindBy()
        {
            UserService userService = new UserService(unitOfWork, unitOfWork.UserRepository);
            User myUser = userService.GetUser(new User
            {
                Username = "rmrgnzlz",
                Password = "1234"
            });
            Assert.IsNotNull(myUser);
        }

        [Test]
        public void TestGetUser()
        {
            UserService userService = new UserService(unitOfWork, unitOfWork.UserRepository);
            User user = new User
            {
                Username = "rmrgnzlz",
                Password = "1234",
            };
            User myUser = userService.GetUser(user);
            Assert.IsNotNull(myUser);
        }

        [Test]
        public void TestGetPermissions()
        {
            IList<Permission> list = unitOfWork.PermissionRepository.GetByRole(1);
            Assert.IsNotNull(list);
        }
    }
}