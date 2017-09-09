using CodingCraftEX06HangFire.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.Models
{
    [Table("Ordens")]
    public class Ordem
    {
        [Key]
        public Guid OrdemId { get; set; }

        [Required]
        public OrdemTipo Tipo { get; set; }

        [DisplayName("Data/hora")]
        public DateTime DataHora { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        public int Quantidade { get; set; }
        
        [Required]
        public Guid AcaoId { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        [ForeignKey(nameof(AcaoId))]
        public virtual Acao Acao { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public virtual Usuario Usuario { get; set; }
        
    }
}