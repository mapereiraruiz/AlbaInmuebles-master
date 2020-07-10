using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteFormasDePagoVM : ObservableViewModel, IPageViewModel
    {
        private FormasDePago entity;
        private HomeFormasDePagoVM baseVM;

        public DeleteFormasDePagoVM(HomeFormasDePagoVM baseVM, FormasDePago entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la Forma De Pago: " + entity?.FormaPago + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Forma De Pago";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.FormasDePago.Find(entity.IdFormaPago);
            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Mantenimientos", "Formas de Pago", model.FormaPago, "Delete", "Ficha Formas de Pago");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Formas De Pago").FirstOrDefault());

        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Formas De Pago").FirstOrDefault();
            viewmodel = new FichaFormasDePagoVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
