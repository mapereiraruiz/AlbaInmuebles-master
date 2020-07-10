using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaLicenciaVM : ObservableViewModel, IPageViewModel
    {
        public Licencias entity;
        private HomeLicenciaVM baseVM;
        public FichaLicenciaVM(HomeLicenciaVM baseVM, Licencias entity = null)
        {
            this.entity = entity ?? new Licencias();
            this.baseVM = baseVM;           
            PageViewModels.Add(new AltaLicenciaVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Licencia";
            }
        }

        public IPageViewModel AltaLicencia
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
