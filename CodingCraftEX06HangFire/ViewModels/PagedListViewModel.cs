using PagedList;

namespace CodingCraftEX06HangFire.ViewModels
{
    public class PagedListViewModel<T>
    {
        public PagedListViewModel()
        {
            Pagina = 1;
            TamanhoPagina = 10;
        }

        public int Pagina { get; set; }

        public int TamanhoPagina { get; set; }

        public IPagedList<T> Resultados { get; set; }
    }
}