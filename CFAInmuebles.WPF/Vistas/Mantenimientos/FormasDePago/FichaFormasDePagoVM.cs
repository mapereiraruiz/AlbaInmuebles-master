using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaFormasDePagoVM : ObservableViewModel, IPageViewModel
    {
        public FormasDePago entity;
        private HomeFormasDePagoVM baseVM;
        public FichaFormasDePagoVM(HomeFormasDePagoVM baseVM, FormasDePago entity = null)
        {
            this.entity = entity ?? new FormasDePago();
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaFormasDePagoVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Formas De Pago";
            }
        }

        public IPageViewModel AltaFormaDePago
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

