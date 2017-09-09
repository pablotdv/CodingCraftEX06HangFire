using CodingCraftEX06HangFire.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.ViewModels
{
    public class AcoesHistoricosViewModel : PagedListViewModel<AcaoHistorico>
    {
        [DisplayName("Data da operação")]
        public DateTime? DataOperacao { get; set; }     
    }
}