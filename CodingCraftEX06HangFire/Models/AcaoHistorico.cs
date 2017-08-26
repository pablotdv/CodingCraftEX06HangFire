using EntityFramework.Triggers;
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

        static AcaoHistorico()
        {
            Triggers<AcaoHistorico>.Inserting += action =>
            {
                action.Entity.Acao.Preco += action.Entity.Preco;
            };
            Triggers<AcaoHistorico>.Updating += action =>
            {
                action.Entity.Acao.Preco += action.Entity.Preco;
            };
        }
    }
}