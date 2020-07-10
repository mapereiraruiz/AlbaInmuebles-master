using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteObraVM : ObservableViewModel, IPageViewModel
    {
        private HistorialObra entity;
        private HomeObraVM baseVM;

        public DeleteObraVM(HomeObraVM baseVM, HistorialObra entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la obra: " + entity?.Descripcion +  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Obra";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                //*** Se borra en cascada, primero ObrasFichero + HistorialObra

                //*** ObrasFichero
                var modelObrasFichero = db.ObrasFichero.Where(m => m.IdHistorialObra == entity.IdHistorialObra).ToList();
                db.ObrasFichero.RemoveRange(modelObrasFichero);
                db.SaveChanges();

                //*** Obras
                var model = db.HistorialObra.Find(entity.IdHistorialObra);
                model.IdUsuarioNavigation = UserId;
                model.FechaEliminacion = DateTime.Now;
                db.SaveChanges();

                Trazabilidad("Mantenimientos", "Obras", model.Descripcion, "Delete", "Ficha Obra");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Obra").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Obra").FirstOrDefault();
            viewmodel = new FichaObraVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
