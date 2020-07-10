using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteConceptoFacturacionVM : ObservableViewModel, IPageViewModel
    {
        private ConceptoFacturacion entity;
        private HomeConceptoFacturacionVM baseVM;

        public DeleteConceptoFacturacionVM(HomeConceptoFacturacionVM baseVM, ConceptoFacturacion entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Concepto de Facturación: " + entity?.Conceptofacturacion1 + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Concepto de Facturación";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.ConceptoFacturacion.Find(entity.IdConceptoFacturacion);
            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Mantenimientos", "Concepto Facturación", model.Conceptofacturacion1, "Delete", "Ficha Concepto Facturación");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Concepto de Facturación").FirstOrDefault());
       
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Concepto de Facturación").FirstOrDefault();
            viewmodel = new FichaConceptoFacturacionVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
