using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteTipoInstalacionVM : ObservableViewModel, IPageViewModel
    {
        private TipoInstalacion entity;
        private HomeTipoInstalacionVM baseVM;

        public DeleteTipoInstalacionVM(HomeTipoInstalacionVM baseVM, TipoInstalacion entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el tipo de instalación: " + entity?.Valor +  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar TipoInstalacion";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.TipoInstalacion.Find(entity.IdTipoInstalacion);

                model.IdUsuarioNavigation = UserId;
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tipo Instalacion", model.Valor, "Delete", "Ficha Tipo Instalacion");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo de Instalación").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo de Instalación").FirstOrDefault();
            viewmodel = new FichaTipoInstalacionVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
