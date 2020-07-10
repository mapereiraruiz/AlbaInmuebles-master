using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteGastosVM : ObservableViewModel, IPageViewModel
    {
        private Gastos entity;
        private HomeGastosVM baseVM;

        public DeleteGastosVM(HomeGastosVM baseVM, Gastos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Gasto: " + entity?.Gasto + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Gasto";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.Gastos.Find(entity.IdGasto);

            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Mantenimientos", "Gastos", model.Gasto, "Delete", "Ficha Gasto");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Gasto").FirstOrDefault());
   
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Gasto").FirstOrDefault();
            viewmodel = new FichaGastosVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
