using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaGastosVM : ObservableViewModel, IPageViewModel
    {
        public Gastos entity;
        private HomeGastosVM baseVM;
        public FichaGastosVM(HomeGastosVM baseVM, Gastos entity = null)
        {
            this.entity = entity ?? new Gastos();
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaGastosVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Gasto";
            }
        }

        public IPageViewModel AltaGasto
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

