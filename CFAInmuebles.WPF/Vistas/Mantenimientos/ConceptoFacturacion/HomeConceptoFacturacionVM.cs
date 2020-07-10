using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeConceptoFacturacionVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeConceptoFacturacionVM()
        {
            PageViewModels.Add(new MantenimientoConceptoFacturacionVM(this));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Home Concepto de Facturación";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeConceptoFacturacion";
            }
        }


        public IPageViewModel MantenimientoConceptoFacturacion
        {
            get { return Acceso("Mantenimiento Concepto de Facturación", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Concepto de Facturación").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
