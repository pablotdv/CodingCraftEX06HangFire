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

        public string RazaoSocial { get; set; }

        public string NomePregao { get; set; }

        public string Segmento { get; set; }

        public string Cnpj { get; set; }

        [InverseProperty(nameof(Acao.Empresa))]
        public ICollection<Acao> Acoes { get; set; }
    }
}