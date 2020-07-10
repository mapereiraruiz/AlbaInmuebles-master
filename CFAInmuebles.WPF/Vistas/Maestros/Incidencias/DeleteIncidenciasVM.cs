using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteIncidenciasVM : ObservableViewModel, IPageViewModel
    {
        private Incidencias entity;
        private HomeIncidenciasVM baseVM;

        public DeleteIncidenciasVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la Incidencia " + entity?.Incidencia + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Incidencias";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.Incidencias.Find(entity.IdIncidencia);

                model.IdUsuarioNavigation = UserId;
                model.FechaEliminacion = DateTime.Now;

                db.SaveChanges();
                Trazabilidad("Maestros", "Incidencias", model.Descripcion, "Delete", "Ficha Incidencias");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Incidencias").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Incidencias").FirstOrDefault();
            viewmodel = new FichaIncidenciasVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
