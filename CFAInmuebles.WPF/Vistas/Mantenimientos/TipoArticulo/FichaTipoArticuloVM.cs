using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaTipoArticuloVM : ObservableViewModel, IPageViewModel
    {
        public TipoArticulos entity;
        private HomeTipoArticuloVM baseVM;
        public FichaTipoArticuloVM(HomeTipoArticuloVM baseVM, TipoArticulos entity = null)
        {
            this.entity = entity ?? new TipoArticulos();
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaTipoArticuloVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Tipo Artículo";
            }
        }

        public IPageViewModel AltaTipoArticulo
        {
            get { return Acceso(0, false); }
        }

        private IPageViewModel Acceso(int indice, bool AdminRequired)
        {

            if (AdminRequired)
                return _pageViewModels[0];

            return _pageViewModels[indice];
        }

    }
}
