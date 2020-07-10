using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteTareaVM : ObservableViewModel, IPageViewModel
    {
        private Tareas entity;
        private HomeTareaVM baseVM;

        public DeleteTareaVM(HomeTareaVM baseVM, Tareas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la Tarea: " + entity?.Descripcion+  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Tarea";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.Tareas.Find(entity.IdTarea);

                model.IdUsuarioNavigation = UserId;
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tareas", model.Descripcion, "Delete", "Ficha Tareas");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tarea").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tarea").FirstOrDefault();
            viewmodel = new FichaTareaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
