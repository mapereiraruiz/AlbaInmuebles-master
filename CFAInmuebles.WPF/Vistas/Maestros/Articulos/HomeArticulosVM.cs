using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeArticulosVM : ObservableViewModel, IPageViewModel, IWindow
    {
       
        public HomeArticulosVM()
        {
            PageViewModels.Add(new MantenimientoArticulosVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Artículos";
        }

        public string Name
        {
            get
            {
                return "Home Artículos";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeArticulos";
            }
        }


        public IPageViewModel MantenimientoArticulos
        {
            get { return Acceso("Mantenimiento Artículos", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Artículos").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
