using CodingCraftEX06HangFire.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraftEX06HangFire.ViewModels
{
    public class EmpresasViewModel : PagedListViewModel<Empresa>
    {
        [Display(Name = "Razão social")]
        public string RazaoSocial { get; set; }
    }
}