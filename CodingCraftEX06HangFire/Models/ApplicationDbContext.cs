using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Z.EntityFramework.Plus;
using System.Web;
using Microsoft.AspNet.Identity;
using EntityFramework.Triggers;
using System.Data.Entity.Validation;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.DynamicFilters;

namespace CodingCraftEX06HangFire.Models
{

    public class ApplicationDbContext : BaseContext
    {
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioAcao>()
               .HasMany(e => e.UsuariosAcoesHistoricos)
               .WithRequired(e => e.UsuarioAcao)
               .HasForeignKey(e => e.UsuarioAcaoId)
               .WillCascadeOnDelete(false);

            modelBuilder.Filter("UsuariosOrdens",
                (Ordem o, Guid usuarioId) => o.UsuarioId == usuarioId,
                () => Guid.Parse(HttpContext.Current.User.Identity.GetUserId()));
            modelBuilder.EnableFilter("UsuariosOrdens", () => HttpContext.Current?.User != null);            
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}