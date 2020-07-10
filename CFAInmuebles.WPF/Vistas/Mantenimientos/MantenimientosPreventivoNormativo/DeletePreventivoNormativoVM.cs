using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeletePreventivoNormativoVM : ObservableViewModel, IPageViewModel
    {
        private Mantenimientos entity;
        private HomePreventivoNormativoVM baseVM;

        public DeletePreventivoNormativoVM(HomePreventivoNormativoVM baseVM, Mantenimientos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el mantenimiento: " + entity?.Servicio+  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar PreventivoNormativo";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.Mantenimientos.Find(entity.IdMantenimiento);

                model.IdUsuarioNavigation = UserId;
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Preventivo Normativo", model.Servicio, "Delete", "Ficha Preventivo Normativo");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Preventivo y Normativo").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha PreventivoNormativo").FirstOrDefault();
            viewmodel = new FichaPreventivoNormativoVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
