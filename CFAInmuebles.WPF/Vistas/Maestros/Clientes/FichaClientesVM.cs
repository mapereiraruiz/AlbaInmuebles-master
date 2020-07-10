using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaClientesVM : ObservableViewModel, IPageViewModel
    {
        public Terceros entity;
        private HomeClientesVM baseVM;

        public FichaClientesVM(HomeClientesVM baseVM, Terceros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaClientesVM(baseVM, entity));
            PageViewModels.Add(new ClienteCuentaFinanzasVM(baseVM, entity));
          
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Clientes";
            }
        }

        public IPageViewModel AltaClientes
        {
            get { return Acceso("Alta Clientes", false); }
        }

        public IPageViewModel ClienteCuentaFinanzas
        {
            get { return Acceso("Cliente Cuenta Finanzas", false); }
        }


       
        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Clientes").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
