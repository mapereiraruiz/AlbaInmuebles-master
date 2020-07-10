using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeNormativasVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeNormativasVM()
        {
            PageViewModels.Add(new MantenimientoNormativasVM(this));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Home Normativas";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeNormativas";
            }
        }

        public IPageViewModel MantenimientoNormativas
        {
            get { return Acceso("Mantenimiento Normativas", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Normativas").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
