using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteContratoClienteVM : ObservableViewModel, IPageViewModel
    {
        private ContratosClientes entity;
        private HomeContratoClienteVM baseVM;

        public DeleteContratoClienteVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Contrato " + entity?.NumeroContrato + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Contratos Clientes";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.ContratosClientes.Find(entity.IdContratoCliente);
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();
                Trazabilidad("Maestros", "Contratos Clientes", model.NumeroContrato.ToString(), "Delete", "Ficha Contratos Clientes");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Contratos Clientes").FirstOrDefault());
             }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Contratos Clientes").FirstOrDefault();
            viewmodel = new FichaContratoClienteVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
