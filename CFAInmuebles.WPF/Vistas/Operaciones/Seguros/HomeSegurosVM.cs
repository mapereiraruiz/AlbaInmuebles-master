using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeSegurosVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeSegurosVM()
        {
            PageViewModels.Add(new MantenimientoSegurosVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Seguros";
        }

        public string Name
        {
            get
            {
                return "Home Seguros";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeSeguros";
            }
        }


        public IPageViewModel MantenimientoSwift
        {
            get { return Acceso("Operaciones Seguros", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Seguros").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
