using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaTipoLicenciaVM : ObservableViewModel, IPageViewModel
    {
        public TipoLicencia entity;
        private HomeTipoLicenciaVM baseVM;
        public FichaTipoLicenciaVM(HomeTipoLicenciaVM baseVM, TipoLicencia entity = null)
        {
            this.entity = entity ?? new TipoLicencia();
            this.baseVM = baseVM;           
            PageViewModels.Add(new AltaTipoLicenciaVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Tipo de Licencia";
            }
        }

        public IPageViewModel AltaTipoLicencia
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
