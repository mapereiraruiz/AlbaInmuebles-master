using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteContactoVM : ObservableViewModel, IPageViewModel
    {
        public Contactos entity;
        private FichaProveedoresVM baseVM;

        public DeleteContactoVM(FichaProveedoresVM baseVM, Contactos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Contacto: " + entity?.Contacto +  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Contacto";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.Contactos.Find(entity.IdContacto);

                model.IdUsuarioNavigation = UserId;
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();

                Trazabilidad("Maestros", "Proveedores", model.Contacto, "Delete", "Ficha Proveedores");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Proveedor Contactos").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Proveedor Contactos").FirstOrDefault());
        }

    }
}
