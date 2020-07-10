using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeSwiftVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeSwiftVM()
        {
            PageViewModels.Add(new MantenimientoSwiftVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Swift";
        }

        public string Name
        {
            get
            {
                return "Home Swift";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeSwift";
            }
        }


        public IPageViewModel MantenimientoSwift
        {
            get { return Acceso("Mantenimiento Swift", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Swift").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
