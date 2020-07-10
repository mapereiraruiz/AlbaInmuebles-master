using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeProvinciasVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeProvinciasVM()
        {
            PageViewModels.Add(new MantenimientoProvinciasVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Provincias";
        }

        public string Name
        {
            get
            {
                return "Home Provincias";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeProvincias";
            }
        }


        public IPageViewModel MantenimientoProvincias
        {
            get { return Acceso("Mantenimiento Provincias", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Provincias").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}

