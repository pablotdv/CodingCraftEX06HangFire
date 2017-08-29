using CodingCraftEX06HangFire.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.Managers
{
    public class ApplicationRoleManager : RoleManager<Grupo, Guid>
    {
        public ApplicationRoleManager(IRoleStore<Grupo, Guid> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<Grupo, Guid, UsuarioGrupo>(context.Get<ApplicationDbContext>()));
        }
    }
}