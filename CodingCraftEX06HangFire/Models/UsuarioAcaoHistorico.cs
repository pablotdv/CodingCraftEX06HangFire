using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.Models
{
    [Table("UsuariosAcoesHistoricos")]
    public class UsuarioAcaoHistorico
    {
        [Key]
        public Guid UsuarioAcaoHistoricoId { get; set; }

        public Guid UsuarioAcaoId { get; set; }

        public Guid AcaoHistoricoId { get; set; }

        [ForeignKey(nameof(UsuarioAcaoId))]
        public virtual UsuarioAcao UsuarioAcao { get; set; }

        [ForeignKey(nameof(AcaoHistoricoId))]
        public virtual AcaoHistorico AcaoHistorico { get; set; }
    }
}