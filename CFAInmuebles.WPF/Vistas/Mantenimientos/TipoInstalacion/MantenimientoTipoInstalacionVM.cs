using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using DevExpress.Data.ODataLinq.Helpers;
//using DevExpress.Data.ODataLinq.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoTipoInstalacionVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeTipoInstalacionVM baseVM;
        private TipoInstalacion _selectedItem;
        private string _valor;
        
        public  string Name
        {
            get
            {
                return "Mantenimiento Tipo de Instalación";
            }
        }

        public MantenimientoTipoInstalacionVM(HomeTipoInstalacionVM baseVM)
        {
            this.baseVM = baseVM;          
        }

        public string Valor
        {
            get { return _valor; }
            set
            {
                if (_valor != value)
                {
                    _valor = value;
                    RaisePropertyChanged("Valor");
                }
            }
        }
        public TipoInstalacion SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((TipoInstalacion)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();
            SearchData();
            Trazabilidad("Mantenimientos", "Tipo Instalacion", "", "Consulta", "Mantenimiento Tipo Instalacion");
        }

        protected void ModifyData(TipoInstalacion entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo de Instalación").FirstOrDefault();
            viewmodel  = new FichaTipoInstalacionVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Tipo Instalacion", "", "Búsqueda", "Cadena de consulta: Tipo Instalación=" + Valor);

            var search = db.TipoInstalacion.Where(m => m.FechaEliminacion == null).AsQueryable();
            if (!String.IsNullOrEmpty(Valor))
                search = search.Where(m => m.Valor.Contains(Valor));
            
            TipoInstalaciones = search.ToList();
        }
    }
}
