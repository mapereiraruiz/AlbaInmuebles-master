using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeListadoAltasVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeListadoAltasVM()
        {
            PageViewModels.Add(new MantenimientoListadoAltasVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Informes - Inventario Listado Altas";
        }

        public string Name
        {
            get
            {
                return "Home Listado Altas";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeListadoAltas";
            }
        }

        public IPageViewModel MantenimientoListadoAltas
        {
            get { return Acceso("Mantenimiento Listado Altas", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Listado Altas").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
