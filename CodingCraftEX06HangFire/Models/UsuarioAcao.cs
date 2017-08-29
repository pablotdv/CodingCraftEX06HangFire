﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftEX06HangFire.Models
{
    [Table("UsuariosAcoes")]
    public class UsuarioAcao
    {
        [Key]
        public Guid UsuarioAcaoId { get; set; }
        
        public Guid UsuarioId { get; set; }

        public Guid AcaoId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey(nameof(AcaoId))]
        public virtual Acao Acao { get; set; }

        public ICollection<UsuarioAcaoHistorico> UsuariosAcoesHistoricos { get; set; }
    }
}