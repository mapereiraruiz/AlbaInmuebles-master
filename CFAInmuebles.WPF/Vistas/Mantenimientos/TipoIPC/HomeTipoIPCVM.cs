using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeTipoIPCVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeTipoIPCVM()
        {
            PageViewModels.Add(new MantenimientoTipoIPCVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Tipo IPC";
        }

        public string Name
        {
            get
            {
                return "Home Tipo IPC";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeTipoIPC";
            }
        }

        public IPageViewModel MantenimientoTiposIPC
        {
            get { return Acceso("Mantenimiento Tipo IPC", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Tipo IPC").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
