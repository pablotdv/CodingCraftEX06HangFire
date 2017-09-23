using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.Models
{
    [Table("OrdensUsuariosAcoes")]
    public class OrdemUsuarioAcao
    {
        [Key]
        public Guid OrdemUsuarioAcaoId { get; set; }

        public Guid OrdemId { get; set; }

        public Guid UsuarioAcaoId { get; set; }

        [ForeignKey(nameof(OrdemId))]
        public virtual Ordem Ordem { get; set; }

        [ForeignKey(nameof(UsuarioAcaoId))]
        public virtual UsuarioAcao UsuarioAcao { get; set; }
    }
}