
using System.Linq;


namespace CFAInmuebles.WPF
{
    public class HomeFacturacionAnualVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeFacturacionAnualVM()
        {
            PageViewModels.Add(new MantenimientoFacturacionAnualVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Informe Facturación Anual";
        }

        public string Name
        {
            get
            {
                return "Home Facuración Anual";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeFacturacionAnual";
            }
        }

        public IPageViewModel MantenimientoFacturacionAnual
        {
            get { return Acceso("Mantenimiento Facturación Anual", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Facturación Anual").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
