using Microsoft.AspNetCore.Mvc;
using OrgRoles.Controllers;
using OrgRoles.Models;

namespace RoleTests
{
    [TestClass]
    public class RoleTests
    {
        RoleController controller;

        RoleTests(RoleController cont) {
            controller = cont;
            }

        [TestMethod]
       public async Task AddRole_ShouldReturn201()
        {
            RoleRequest rq=new RoleRequest()
            { 
            Name="TEST",
             Description="dsaallnda alkdnas"
            };
            ActionResult<Role> response= await controller.CreateRole(rq);
            Assert.IsNotNull(response);


        }
    }
}