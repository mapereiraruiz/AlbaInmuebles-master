using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeFacturacionVM : ObservableViewModel, IPageViewModel, IWindow
    {
       
        public HomeFacturacionVM()
        {
            PageViewModels.Add(new MantenimientoFacturacionVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Facturación";
        }

        public string Name
        {
            get
            {
                return "Home Facturacion";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeFacturacion";
            }
        }


        public IPageViewModel MantenimientoFacturacion
        {
            get { return Acceso("Mantenimiento Facturacion", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Facturación").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
