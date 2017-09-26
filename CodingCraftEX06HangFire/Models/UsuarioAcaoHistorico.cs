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
        
        [ForeignKey(nameof(UsuarioAcaoId))]
        public virtual UsuarioAcao UsuarioAcao { get; set; }

        [Required]
        [Display(Name = "Preço da ação R$")]
        public decimal Preco { get; set; }

        [Required]
        [Display(Name = "Variação R$")]
        public decimal ValorVariacao { get; set; }

        [Required]
        [Display(Name = "% Variação")]
        public decimal PercentualVariacao { get; set; }

        public decimal Rentabilidade { get; set; }
    }
}