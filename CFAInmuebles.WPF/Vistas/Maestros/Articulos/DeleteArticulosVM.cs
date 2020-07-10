using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteArticulosVM : ObservableViewModel, IPageViewModel
    {
        private Articulos entity;
        private HomeArticulosVM baseVM;

        public DeleteArticulosVM(HomeArticulosVM baseVM, Articulos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Artículo " + entity?.Articulo + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Artículos";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.Articulos.Find(entity.IdArticulo);
            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Maestros", "Artículos", model.Articulo,  "Delete", "Ficha Artículos");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Artículos").FirstOrDefault());
            
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Artículos").FirstOrDefault();
            viewmodel = new FichaArticulosVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
