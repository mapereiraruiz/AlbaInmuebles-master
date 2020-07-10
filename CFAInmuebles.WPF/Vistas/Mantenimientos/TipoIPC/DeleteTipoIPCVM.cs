using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DeleteTipoIPCVM : ObservableViewModel, IPageViewModel
    {
        private TipoIpc entity;
        private HomeTipoIPCVM baseVM;

        public DeleteTipoIPCVM(HomeTipoIPCVM baseVM, TipoIpc entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Está seguro que quiere eliminar el Tipo de IPC: " + entity?.Ipc +  "?";
        }

        public string Name
        {
            get
            {
                return "Eliminar Tipo IPC";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.TipoIpc.Find(entity.IdTipoIpc);
            model.FechaEliminacion = DateTime.Now;
            model.IdUsuarioNavigation = UserId;
            db.SaveChanges();
            Trazabilidad("Mantenimientos", "Tipos IPC", model.Ipc, "Delete", "Ficha Tipo IPC");
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo IPC").FirstOrDefault());
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo IPC").FirstOrDefault();
            viewmodel = new FichaTipoIPCVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
