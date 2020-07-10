using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeProveedoresVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeProveedoresVM()
        {
            PageViewModels.Add(new MantenimientoProveedoresVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Proveedores";
        }

        public string Name
        {
            get
            {
                return "Home Proveedores";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeProveedores";
            }
        }

        public IPageViewModel MantenimientoProveedores
        {
            get { return Acceso("Mantenimiento Proveedores", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Proveedores").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
