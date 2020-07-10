using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteTipoIVAVM : ObservableViewModel, IPageViewModel
    {
        private TipoIva entity;
        private HomeTipoIVAVM baseVM;

        public DeleteTipoIVAVM(HomeTipoIVAVM baseVM, TipoIva entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Tipo de IVA: " + entity?.TipoIva1 + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Tipo IVA";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.TipoIva.Find(entity.IdTipoIva);

            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Mantenimientos", "Tipos IVA", model.TipoIva1, "Delete", "Ficha Tipo IVA");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo IVA").FirstOrDefault());
   
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo IVA").FirstOrDefault();
            viewmodel = new FichaTipoIVAVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
