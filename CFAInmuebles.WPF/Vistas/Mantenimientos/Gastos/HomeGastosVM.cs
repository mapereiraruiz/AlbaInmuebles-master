using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeGastosVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeGastosVM()
        {
            PageViewModels.Add(new MantenimientoGastosVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Gastos";
        }
        public string Name
        {
            get
            {
                return "Home Gastos";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeGastos";
            }
        }
    }
}
