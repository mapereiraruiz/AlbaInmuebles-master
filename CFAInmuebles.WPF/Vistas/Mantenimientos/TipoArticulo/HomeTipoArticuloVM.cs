using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeTipoArticuloVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public HomeTipoArticuloVM()
        {
            PageViewModels.Add(new MantenimientoTipoArticuloVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Tipo Artículo";
        }

        public string Name
        {
            get
            {
                return "Home Tipo Artículo";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeTipoArticulo";
            }
        }


        public IPageViewModel MantenimientoTipoArticulo
        {
            get { return Acceso("Mantenimiento Tipo Artículo", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Tipo Artículo").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
