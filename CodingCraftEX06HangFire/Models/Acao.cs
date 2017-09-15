using FileHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftEX06HangFire.Models
{
    [Table("Acoes")]
    [DelimitedRecord(";")]
    public class Acao
    {
        [Key]
        public Guid AcaoId { get; set; }

        public Guid EmpresaId { get; set; }

        [Required]
        [Display(Name ="Código de negociação")]
        public string CodigoNegociacao { get; set; }

        [Required]
        [Display(Name = "Código ISIN")]
        public string CodigoIsin { get; set; }

        [Required]
        [Display(Name = "Preço atual")]
        public decimal Preco { get; set; }

        [ForeignKey(nameof(EmpresaId))]
        public virtual Empresa Empresa { get; set; }

        [InverseProperty(nameof(UsuarioAcao.Acao))]
        public ICollection<UsuarioAcao> UsuariosAcoes { get; set; }

        [InverseProperty(nameof(AcaoHistorico.Acao))]
        public ICollection<AcaoHistorico> AcoesHistoricos { get; set; }

        [InverseProperty(nameof(Ordem.Acao))]
        public ICollection<Ordem> Ordens { get; set; }
    }
}