using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaTipoIVAVM : ObservableViewModel, IPageViewModel
    {
        public TipoIva entity;
        private HomeTipoIVAVM baseVM;
        public FichaTipoIVAVM(HomeTipoIVAVM baseVM, TipoIva entity = null)
        {
            this.entity = entity ?? new TipoIva();
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaTipoIVAVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Tipo IVA";
            }
        }

        public IPageViewModel AltaTipoIVA
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

