using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeLicenciaVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeLicenciaVM()
        {
            PageViewModels.Add(new MantenimientoLicenciaVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Licencias";
        }

        public string Name
        {
            get
            {
                return "Home Licencias";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeLicencia";
            }
        }

        public IPageViewModel MantenimientoLicencias
        {
            get { return Acceso("Mantenimiento Licencias", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Licencias").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
