using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoTipoIPCVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeTipoIPCVM baseVM;
        private TipoIpc _selectedItem;
        private string _descripcion;

        public  string Name
        {
            get
            {
                return "Mantenimiento Tipo IPC";
            }
        }

        public MantenimientoTipoIPCVM(HomeTipoIPCVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                if (_descripcion != value)
                {
                    _descripcion = value;
                    RaisePropertyChanged("Descripcion");
                }
            }
        }
        public TipoIpc SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData((TipoIpc)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();        
            SearchData();
            Trazabilidad("Mantenimientos", "Tipos IPC", "", "Consulta", "Mantenimiento Tipo IPC");           
        }

        protected void ModifyData(TipoIpc entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo IPC").FirstOrDefault();
            viewmodel  = new FichaTipoIPCVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Tipos IPC", "", "Búsqueda", "Cadena de consulta: Descripción=" + Descripcion);

            TipoIpcs = db.TipoIpc.Where(m => m.FechaEliminacion == null).ToList();

            if (!String.IsNullOrEmpty(Descripcion))
                TipoIpcs = TipoIpcs.Where(m => m.Ipc.Contains(Descripcion)).ToList();
        }
    }
}
