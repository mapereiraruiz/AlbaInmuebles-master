using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeEmpresasVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeEmpresasVM()
        {
            PageViewModels.Add(new MantenimientoEmpresasVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Empresas";
        }

        public string Name
        {
            get
            {
                return "Home Empresas";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeEmpresas";
            }
        }


        public IPageViewModel MantenimientoEmpresas
        {
            get { return Acceso("Mantenimiento Empresas", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Empresas").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
