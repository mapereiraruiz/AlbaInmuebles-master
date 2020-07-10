using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeClientesVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeClientesVM()
        {
            PageViewModels.Add(new MantenimientoClientesVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Clientes";
        }

        public string Name
        {
            get
            {
                return "Home Clientes";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeClientes";
            }
        }


        public IPageViewModel MantenimientoClientes
        {
            get { return Acceso("Mantenimiento Clientes", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Clientes").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
