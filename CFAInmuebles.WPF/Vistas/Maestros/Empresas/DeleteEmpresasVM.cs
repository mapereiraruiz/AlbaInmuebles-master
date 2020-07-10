using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class DeleteEmpresasVM : ObservableViewModel, IPageViewModel
    {
        private Empresas entity;
        private HomeEmpresasVM baseVM;

        public DeleteEmpresasVM(HomeEmpresasVM baseVM, Empresas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar la Empresa " + entity?.Empresa + "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Empresas";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.Empresas.Find(entity.IdEmpresa);

            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Maestros", "Empresas", model.Empresa, "Delete", "Ficha Empresas");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Empresas").FirstOrDefault());
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Empresas").FirstOrDefault();
            viewmodel = new FichaEmpresasVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
