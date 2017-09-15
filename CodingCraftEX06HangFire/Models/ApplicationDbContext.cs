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

    public class ApplicationDbContext : IdentityDbContext<Usuario, Grupo, Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentidade>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            int rowAffecteds = 0;
            try
            {
                var audit = new Audit()
                {
                    CreatedBy = HttpContext.Current?.User.Identity.GetUserName() ?? "Migrations"
                };
                audit.PreSaveChanges(this);

                // Coloque funções extras, como preenchimento automático de campos, aqui.
                rowAffecteds = this.SaveChangesWithTriggers(base.SaveChanges);

                audit.PostSaveChanges();
                if (audit.Configuration.AutoSavePreAction != null)
                {
                    audit.Configuration.AutoSavePreAction(this, audit);
                    base.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionsMessage = string.Concat(ex.Message, "Os erros de validações são: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionsMessage, ex.EntityValidationErrors);
            }

            return rowAffecteds;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            int rowAffecteds = 0;

            try
            {
                var audit = new Audit();
                audit.PreSaveChanges(this);

                // Coloque funções extras, como preenchimento automático de campos, aqui.
                rowAffecteds = await this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, cancellationToken).ConfigureAwait(false);

                audit.PostSaveChanges();
                if (audit.Configuration.AutoSavePreAction != null)
                {
                    audit.Configuration.AutoSavePreAction(this, audit);
                    await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionsMessage = string.Concat(ex.Message, "Os erros de validações são: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionsMessage, ex.EntityValidationErrors);
            }

            return rowAffecteds;
        }

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

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Acao> Acoes { get; set; }

        public DbSet<AcaoHistorico> AcoesHistoricos { get; set; }

        public DbSet<Ordem> Ordens { get; set; }

        public DbSet<UsuarioAcao> UsuariosAcoes { get; set; }

        public DbSet<UsuarioAcaoHistorico> UsuariosAcoesHistoricos { get; set; }
    }
}