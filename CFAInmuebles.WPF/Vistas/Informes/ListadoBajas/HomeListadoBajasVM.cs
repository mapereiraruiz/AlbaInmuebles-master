using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeListadoBajasVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeListadoBajasVM()
        {
            PageViewModels.Add(new MantenimientoListadoBajasVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Licencias";
        }

        public string Name
        {
            get
            {
                return "Home Listado Bajas";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeListadoBajas";
            }
        }

        public IPageViewModel MantenimientoLicencias
        {
            get { return Acceso("Mantenimiento Listado Bajas", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Listado Bajas").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
