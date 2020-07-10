using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteTareaPeriodicaVM : ObservableViewModel, IPageViewModel
    {
        private TareaPeriodica entity;
        private HomeTareaPeriodicaVM baseVM;

        public DeleteTareaPeriodicaVM(HomeTareaPeriodicaVM baseVM, TareaPeriodica entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la Tarea Periódica: " + entity?.Descripcion+  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Tarea Periódica";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.TareaPeriodica.Find(entity.IdTareaPeriodica);

                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();
                Trazabilidad("Maestros", "Tareas Periódicas", model.Descripcion, "Delete", "Ficha Tarea Periódica");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tarea Periódica").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tarea Periódica").FirstOrDefault();
            viewmodel = new FichaTareaPeriodicaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
