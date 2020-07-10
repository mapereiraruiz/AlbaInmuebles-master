using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaTipoIPCVM : ObservableViewModel, IPageViewModel
    {
        public TipoIpc entity;
        private HomeTipoIPCVM baseVM;
        public FichaTipoIPCVM(HomeTipoIPCVM baseVM, TipoIpc entity = null)
        {
            this.entity = entity ?? new TipoIpc();
            this.baseVM = baseVM;           
            PageViewModels.Add(new AltaTipoIPCVM(baseVM, this.entity));
            PageViewModels.Add(new TiposIPCContratosVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Tipo IPC";
            }
        }

        public IPageViewModel AltaTipoIPC
        {
            get { return Acceso(0, false); }
        }

        public IPageViewModel TiposIPCContratos
        {
            get { return Acceso(1, false); }
        }

        private IPageViewModel Acceso(int indice, bool AdminRequired)
        {

            if (AdminRequired)
                return _pageViewModels[0];

            return _pageViewModels[indice];
        }
    }
}
