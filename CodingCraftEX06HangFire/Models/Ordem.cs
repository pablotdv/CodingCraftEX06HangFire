﻿using CodingCraftEX06HangFire.Models.Enums;
using EntityFramework.Triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
        public decimal Total { get; set; }

        [Required]
        public Guid AcaoId { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }
        
        public decimal Chance { get; set; }

        public decimal? Rentabilidade { get; set; }

        [ForeignKey(nameof(AcaoId))]
        public virtual Acao Acao { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public virtual Usuario Usuario { get; set; }

        [InverseProperty(nameof(OrdemUsuarioAcao.Ordem))]
        public virtual ICollection<OrdemUsuarioAcao> OrdensUsuariosAcoes { get; set; }
        

        static Ordem()
        {
            Triggers<Ordem>.Inserting += action =>
            {
                action.Entity.Total = action.Entity.Preco * action.Entity.Quantidade;
            };

            Triggers<Ordem>.Updating += action =>
            {
                action.Entity.Total = action.Entity.Preco * action.Entity.Quantidade;
            };
        }
    }
}