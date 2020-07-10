using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteSwiftVM : ObservableViewModel, IPageViewModel
    {
        private Swift entity;
        private HomeSwiftVM baseVM;

        public DeleteSwiftVM(HomeSwiftVM baseVM, Swift entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Swift: " + entity?.Swift1 + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Swift";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.Swift.Find(entity.IdSwift);

                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Swift", model.Swift1, "Delete", "Ficha Swift");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Swift").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Swift").FirstOrDefault();
            viewmodel = new FichaSwiftVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
