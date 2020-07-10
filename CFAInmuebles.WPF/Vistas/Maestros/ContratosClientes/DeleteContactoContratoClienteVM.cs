using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteContactoContratoClienteVM : ObservableViewModel, IPageViewModel
    {
        public Contactos entity;
        private FichaContratoClienteVM baseVM;

        public DeleteContactoContratoClienteVM(FichaContratoClienteVM baseVM, Contactos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Contacto: " + entity?.Contacto +  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Contacto Contrato Cliente";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity.IdContacto > 0)
            {
                var model = db.Contactos.Find(entity.IdContacto);

                model.IdUsuarioNavigation = UserId;
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();

                Trazabilidad("Maestros", "Contratos Clientes", model.Contacto, "Delete", "Ficha Contratos Clientes Contactos");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Contratos Clientes Contactos").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Contratos Clientes Contactos").FirstOrDefault());
        }

    }
}
