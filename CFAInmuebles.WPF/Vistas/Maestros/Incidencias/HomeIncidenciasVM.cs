using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeIncidenciasVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeIncidenciasVM()
        {
            PageViewModels.Add(new MantenimientoIncidenciasVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Incidencias";
        }

        public string Name
        {
            get
            {
                return "Home Incidencias";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeIncidencias";
            }
        }


        public IPageViewModel MantenimientoIncidencias
        {
            get { return Acceso("Mantenimiento Incidencias", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Incidencias").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
