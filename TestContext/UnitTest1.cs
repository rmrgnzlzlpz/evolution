using Infraestructura.Interfaces;
using Infraestructura.Utils;
using NUnit.Framework;
using Domain.Entities;

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
            unitOfWork.Init();
            User user = new User
            {
                Username = "Kenneth",
                Password = "qwertyuiop",
                Firstname = "Kenneth",
                Lastname = "Mendoza",
                RoleId = 8,
                State = UserState.Active
            };
            User savedUser = unitOfWork.UserRepository.Add(user);
            unitOfWork.Save();
            Assert.IsNotNull(savedUser);
        }

        [Test]
        public void TestUserFind()
        {
            long id = 4;
            unitOfWork.Init();
            User user = unitOfWork.UserRepository.Find(id);
            unitOfWork.Save();
            Assert.IsNotNull(user);
        }
    }
}