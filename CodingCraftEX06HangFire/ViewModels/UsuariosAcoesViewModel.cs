﻿using CodingCraftEX06HangFire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.ViewModels
{
    public class UsuariosAcoesViewModel : PagedListViewModel<UsuarioAcao>
    {
        public decimal Rentabilidade { get; set; }
    }
}