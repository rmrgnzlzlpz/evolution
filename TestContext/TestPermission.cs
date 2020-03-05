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
        UnitOfWork uow = new UnitOfWork();
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
