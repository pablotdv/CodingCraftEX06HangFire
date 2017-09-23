using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodingCraftEX06HangFire.Models;

namespace CodingCraftEX06HangFire.ViewModels
{
    public class VendaViewModel
    {        
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public Acao Acao { get; set; }
        public UsuarioAcao UsuarioAcao { get; set; }
    }
}