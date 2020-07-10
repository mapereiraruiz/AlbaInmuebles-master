using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteContratoProveedorVM : ObservableViewModel, IPageViewModel
    {
        private ContratosProveedores entity;
        private HomeContratoProveedorVM baseVM;

        public DeleteContratoProveedorVM(HomeContratoProveedorVM baseVM, ContratosProveedores entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Contrato " + entity?.ReferenciaContrato + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Contratos Proveedores";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.ContratosProveedores.Find(entity.IdContratoProveedor);
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();
                Trazabilidad("Maestros", "Contratos Proveedores", model.ReferenciaContrato, "Delete", "Ficha Contratos Clientes");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Contratos Clientes").FirstOrDefault());
             }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Contratos Proveedores").FirstOrDefault();
            viewmodel = new FichaContratoProveedorVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
