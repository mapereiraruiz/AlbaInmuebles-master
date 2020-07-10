using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeTipoInstalacionVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeTipoInstalacionVM()
        {
            PageViewModels.Add(new MantenimientoTipoInstalacionVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Tipo de Instalación";
        }

        public string Name
        {
            get
            {
                return "Home Tipo de Instalación";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeTipoInstalacion";
            }
        }

        public IPageViewModel MantenimientoTipoInstalacion
        {
            get { return Acceso("Mantenimiento Tipo de Instalación", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Tipo de Instalación").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
