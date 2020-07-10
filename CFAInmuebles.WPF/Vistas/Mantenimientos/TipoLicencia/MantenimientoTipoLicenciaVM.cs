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
    public class MantenimientoTipoLicenciaVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeTipoLicenciaVM baseVM;
        private TipoLicencia _selectedItem;
        private string _valor;
        
        public  string Name
        {
            get
            {
                return "Mantenimiento Tipo de Licencia";
            }
        }

        public MantenimientoTipoLicenciaVM(HomeTipoLicenciaVM baseVM)
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
        public TipoLicencia SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((TipoLicencia)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            SearchData();
            Trazabilidad("Mantenimientos", "Tipo Licencia", "", "Consulta", "Mantenimiento Tipo Licencia");
        }

        protected void ModifyData(TipoLicencia entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo de Licencia").FirstOrDefault();
            viewmodel = new FichaTipoLicenciaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Tipo Licencia", "", "Búsqueda", "Cadena de consulta: Tipo Licencia=" + Valor);

            var search = db.TipoLicencia.Where(m => m.FechaEliminacion == null).AsQueryable();
            
            if (!String.IsNullOrEmpty(Valor))
                search = search.Where(m => m.Valor.Contains(Valor));
            TiposLicencia = search.ToList();
        }
    }
}
