using CodingCraftEX06HangFire.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.ViewModels
{
    public class AcoesViewModel : PagedListViewModel<Acao>
    {
        [DisplayName("Código da negociação")]
        public string CodigoNegociacao { get; set; }
    }
}