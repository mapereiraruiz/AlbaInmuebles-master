using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteProveedoresVM : ObservableViewModel, IPageViewModel
    {
        private Terceros entity;
        private HomeProveedoresVM baseVM;

        public DeleteProveedoresVM(HomeProveedoresVM baseVM, Terceros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Proveedor: " + entity?.Nombre+  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Proveedores";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Proveedores").FirstOrDefault();
            viewmodel = new FichaProveedoresVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
