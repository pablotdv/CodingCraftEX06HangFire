using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodingCraftEX06HangFire.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Usuario : IdentityUser<Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentidade>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Usuario, Guid> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [DisplayName("Dinheiro R$")]
        public decimal Dinheiro { get; set; }

        [DisplayName("Saldo R$")]
        public decimal Saldo { get; set; }

        [InverseProperty(nameof(UsuarioAcao.Usuario))]
        public ICollection<UsuarioAcao> UsuariosAcoes { get; set; }

        [InverseProperty(nameof(Ordem.Usuario))]
        public ICollection<Ordem> Ordens { get; set; }
    }
}