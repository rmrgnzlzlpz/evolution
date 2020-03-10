using Aplicacion.Services;
using Infraestructura.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestContext
{
    class TestPermission
    {
        UnitOfWork uow = new UnitOfWork("Data Source=SQL5052.site4now.net;Initial Catalog=DB_A561BE_evolution;User Id=DB_A561BE_evolution_admin;Password=Ramiroe123;");
        PermissionService service;

        [SetUp]
        public void Setup()
        {
            service = new PermissionService(uow, uow.PermissionRepository);
        }
        [Test]
        public void Test1()
        {
            
            //var test = service.IsOfRole()
        }
    }
}
