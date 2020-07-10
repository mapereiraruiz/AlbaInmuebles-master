using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeTipoLicenciaVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeTipoLicenciaVM()
        {
            PageViewModels.Add(new MantenimientoTipoLicenciaVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Tipo de Licencia";
        }

        public string Name
        {
            get
            {
                return "Home TipoLicencia";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeTipoLicencia";
            }
        }

        public IPageViewModel MantenimientoTipoLicencia
        {
            get { return Acceso("Mantenimiento Tipo de Licencia", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home TipoLicencia").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
