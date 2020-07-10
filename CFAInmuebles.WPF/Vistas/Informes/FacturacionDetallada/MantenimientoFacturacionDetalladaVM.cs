using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoFacturacionDetalladaVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeFacturacionDetalladaVM baseVM;
        private Facturacion _selectedItem;
        private string _descripcion;

        public  string Name
        {
            get
            {
                return "Mantenimiento Facturación Detallada";
            }
        }

        public MantenimientoFacturacionDetalladaVM(HomeFacturacionDetalladaVM baseVM)
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
        public Facturacion SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ExportData((Facturacion)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();        
            SearchData();
            Trazabilidad("Informes", "Facturación Detallada", "", "Consulta", "Mantenimiento Facturación Detallada");           
        }

        protected void ExportData(Facturacion entity)
        {
            
        }
        
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Infomes", "Facturación Detallada", "", "Búsqueda", "Cadena de consulta: Descripción=" + Descripcion);
        }
    }
}
