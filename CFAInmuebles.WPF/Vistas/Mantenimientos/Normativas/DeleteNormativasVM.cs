using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteNormativasVM : ObservableViewModel, IPageViewModel
    {
        private Normas entity;
        private HomeNormativasVM baseVM;

        public DeleteNormativasVM(HomeNormativasVM baseVM, Normas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la Normativa: " + entity?.TextoDescriptivo + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Normativas";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.Normas.Find(entity.IdNorma);

            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Mantenimientos", "Normativas", model.TextoDescriptivo, "Delete", "Ficha Normativas");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Normativas").FirstOrDefault());   
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Normativas").FirstOrDefault();
            viewmodel = new FichaNormativasVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
