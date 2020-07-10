using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeTipoIVAVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeTipoIVAVM()
        {
            PageViewModels.Add(new MantenimientoTipoIVAVM(this));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Home Tipo IVA";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeTipoIVA";
            }
        }
        public IPageViewModel MantenimientoTipoIVA
        {
            get { return Acceso("Mantenimiento Tipo IVA", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Tipo IVA").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
