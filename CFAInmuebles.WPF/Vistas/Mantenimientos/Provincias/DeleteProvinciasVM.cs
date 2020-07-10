using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteProvinciasVM : ObservableViewModel, IPageViewModel
    {
        private Provincias entity;
        private HomeProvinciasVM baseVM;

        public DeleteProvinciasVM(HomeProvinciasVM baseVM, Provincias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la Provincia: " + entity?.Provincia + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Provincias";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.Provincias.Find(entity.IdProvincia);

                model.FechaEliminacion = DateTime.Now;
                model.IdUsuarioNavigation = UserId;
                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Provincias", model.Provincia, "Delete", "Ficha Provincia");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Provincias").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Provincias").FirstOrDefault();
            viewmodel = new FichaProvinciasVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
