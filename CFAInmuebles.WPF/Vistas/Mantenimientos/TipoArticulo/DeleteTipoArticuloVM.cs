using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteTipoArticuloVM : ObservableViewModel, IPageViewModel
    {
        private TipoArticulos entity;
        private HomeTipoArticuloVM baseVM;

        public DeleteTipoArticuloVM(HomeTipoArticuloVM baseVM, TipoArticulos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Tipo de Articulo: " + entity?.Tipoarticulo + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Tipo Artículo";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.TipoArticulos.Find(entity.IdTipoArticulo);
            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Mantenimientos", "Tipo Artículo", model.Tipoarticulo, "Delete", "Ficha Tipo Artículo");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo Artículo").FirstOrDefault());
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo Artículo").FirstOrDefault();
            viewmodel = new FichaTipoArticuloVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
