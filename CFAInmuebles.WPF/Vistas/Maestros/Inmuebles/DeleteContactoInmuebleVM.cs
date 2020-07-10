using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteContactoInmuebleVM : ObservableViewModel, IPageViewModel
    {
        public Contactos entity;
        private FichaInmueblesVM baseVM;

        public DeleteContactoInmuebleVM(FichaInmueblesVM baseVM, Contactos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Contacto: " + entity?.Contacto +  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Contacto Inmueble";
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

                Trazabilidad("Maestros", "Inmuebles", model.Contacto, "Delete", "Ficha Inmuebles Contactos");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Inmueble Contactos").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Inmueble Contactos").FirstOrDefault());
        }

    }
}
