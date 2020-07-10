using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeFacturacionDetalladaVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeFacturacionDetalladaVM()
        {
            PageViewModels.Add(new MantenimientoFacturacionDetalladaVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Informe Facturación Detallada";
        }

        public string Name
        {
            get
            {
                return "Home Facturación Detallada";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeFacturacionDetallada";
            }
        }

        public IPageViewModel MantenimientoFacturacionDetallada
        {
            get { return Acceso("Mantenimiento Facturación Detallada", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Facturación Detallada").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
