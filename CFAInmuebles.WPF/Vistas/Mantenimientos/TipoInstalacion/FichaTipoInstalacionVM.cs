using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaTipoInstalacionVM : ObservableViewModel, IPageViewModel
    {
        public TipoInstalacion entity;
        private HomeTipoInstalacionVM baseVM;
        public FichaTipoInstalacionVM(HomeTipoInstalacionVM baseVM, TipoInstalacion entity = null)
        {
            this.entity = entity ?? new TipoInstalacion();
            this.baseVM = baseVM;           
            PageViewModels.Add(new AltaTipoInstalacionVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Tipo de Instalación";
            }
        }

        public IPageViewModel AltaTipoInstalacion
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
