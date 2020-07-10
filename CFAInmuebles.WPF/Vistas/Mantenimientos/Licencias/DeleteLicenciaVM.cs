using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteLicenciaVM : ObservableViewModel, IPageViewModel
    {
        private Licencias entity;
        private HomeLicenciaVM baseVM;

        public DeleteLicenciaVM(HomeLicenciaVM baseVM, Licencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la licencia: " + entity?.Descripcion +  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Licencia";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.Licencias.Find(entity.IdLicencia);

                model.IdUsuarioNavigation = UserId;
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Licencias", model.Descripcion, "Delete", "Ficha Licencias");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Licencia").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Licencia").FirstOrDefault();
            viewmodel = new FichaLicenciaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
