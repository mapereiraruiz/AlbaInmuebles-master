using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteTipoLicenciaVM : ObservableViewModel, IPageViewModel
    {
        private TipoLicencia entity;
        private HomeTipoLicenciaVM baseVM;

        public DeleteTipoLicenciaVM(HomeTipoLicenciaVM baseVM, TipoLicencia entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el tipo de licencia: " + entity?.Valor +  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar TipoLicencia";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.TipoLicencia.Find(entity.IdTipoLicencia);

            model.IdUsuarioNavigation = UserId;
            model.FechaEliminacion = DateTime.Now;
            db.SaveChanges();
            Trazabilidad("Mantenimientos", "Tipo Licencia", model.Valor, "Delete", "Ficha Tipo Licencia");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo de Licencia").FirstOrDefault());
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo de Licencia").FirstOrDefault();
            viewmodel = new FichaTipoLicenciaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
