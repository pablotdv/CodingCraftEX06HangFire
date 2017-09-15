using EntityFramework.Triggers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Z.EntityFramework.Plus;

namespace CodingCraftEX06HangFire.Models
{
    public class BaseContext : IdentityDbContext<Usuario, Grupo, Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentidade>
    {
        public BaseContext()
            : base("DefaultConnection")
        {
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

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Acao> Acoes { get; set; }

        public DbSet<AcaoHistorico> AcoesHistoricos { get; set; }

        public DbSet<Ordem> Ordens { get; set; }

        public DbSet<UsuarioAcao> UsuariosAcoes { get; set; }

        public DbSet<UsuarioAcaoHistorico> UsuariosAcoesHistoricos { get; set; }
    }
}