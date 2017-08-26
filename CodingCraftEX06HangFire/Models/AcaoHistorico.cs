using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftEX06HangFire.Models
{
    [Table("AcoesHistoricos")]
    public class AcaoHistorico
    {
        [Key]
        public Guid AcaoHistoricoId { get; set; }

        [Required]
        public Guid AcaoId { get; set; }

        [Required]
        public decimal Preco { get; set; }
        
        [Required]
        public DateTime DataHora { get; set; }

        [ForeignKey(nameof(AcaoId))]
        public virtual Acao Acao { get; set; }
    }
}