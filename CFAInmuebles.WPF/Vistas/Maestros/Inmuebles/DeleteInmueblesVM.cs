using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteInmueblesVM : ObservableViewModel, IPageViewModel
    {
        private Inmuebles entity;
        private HomeInmueblesVM baseVM;

        public DeleteInmueblesVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Inmueble " + entity?.Inmueble + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Inmuebles";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.Inmuebles.Find(entity.IdEmpresa);
            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Maestros", "Inmuebles", model.Inmueble, "Delete", "Ficha Inmuebles");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Inmuebles").FirstOrDefault());
       
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Inmuebles").FirstOrDefault();
            viewmodel = new FichaInmueblesVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
