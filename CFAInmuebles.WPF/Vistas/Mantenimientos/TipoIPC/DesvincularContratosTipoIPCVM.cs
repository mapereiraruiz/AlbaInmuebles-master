using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class DesvincularContratosTipoIPCVM : ObservableViewModel, IPageViewModel
    {
        private TipoIpc entity;
        private HomeTipoIPCVM baseVM;

        public DesvincularContratosTipoIPCVM(HomeTipoIPCVM baseVM, TipoIpc entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            TextDeleteItem = "¿Esta seguro que quiere desvincular los contratos del Tipo de IPC: " + entity?.Ipc +  "?";
        }

        public string Name
        {
            get
            {
                return "Desvincular Contratos Tipo IPC";
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.TipoIpc.Find(entity.IdTipoIpc);

            //Comprobamos los contratos que tiene vinculado
            var contratos = db.ContratosClientes.Where(m => m.IdTipoIpc == entity.IdTipoIpc).ToList();

            if (contratos.Count() == 0)
            {
                Mensaje = "Este Tipo IPC no tiene ningún contrato vinculado.";
            }
            else
            {
                foreach (var contrato in contratos)
                {
                    contrato.IdTipoIpcNavigation = null;
                    contrato.FechaSistema = DateTime.Now;
                    contrato.IdUsuarioNavigation = UserId;
                }

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tipos IPC", model.Ipc, "Update", "Desvincular Contratos");
                Mensaje = "Los contratos han sido desvinculados del Tipo IPC.";
            }
 
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
