using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeFormasDePagoVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeFormasDePagoVM()
        {
            PageViewModels.Add(new MantenimientoFormasDePagoVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Formas De Pago";
        }

        public string Name
        {
            get
            {
                return "Home Formas De Pago";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeFormasDePago";
            }
        }


        public IPageViewModel MantenimientoFormasDePago
        {
            get { return Acceso("Mantenimiento Formas De Pago", false); }
        }


        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Formas De Pago").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
