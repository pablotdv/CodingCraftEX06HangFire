using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.Models
{
    [Table("Empresas")]
    public class Empresa
    {
        [Key]
        public Guid EmpresaId { get; set; }

        [Display(Name = "Razão social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome de pregão")]
        public string NomePregao { get; set; }
                
        [Display(Name = "CNPJ")]
        [MaxLength(14)]
        [Required]
        public string Cnpj { get; set; }

        [InverseProperty(nameof(Acao.Empresa))]
        public ICollection<Acao> Acoes { get; set; }
    }
}