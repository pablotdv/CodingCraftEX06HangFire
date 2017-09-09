using CodingCraftEX06HangFire.Models;
using CodingCraftEX06HangFire.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.ViewModels
{
    public class OrdensViewModel : PagedListViewModel<Ordem>
    {        
        public OrdemTipo? Tipo { get; set; }
    }
}