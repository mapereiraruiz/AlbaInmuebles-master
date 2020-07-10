using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaProvinciasVM : ObservableViewModel, IPageViewModel
    {
        public Provincias entity;
        private HomeProvinciasVM baseVM;
        public FichaProvinciasVM(HomeProvinciasVM baseVM, Provincias entity = null)
        {
            this.entity = entity ?? new Provincias();
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaProvinciasVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Provincias";
            }
        }

        public IPageViewModel AltaProvincias
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

