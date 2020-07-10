using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ArticuloHistorialOcupacionVM : ObservableViewModel, IPageViewModel
    {
        public Articulos entity;

        protected new ICommand _modifyCommand;

        private HistorialOcupacion _selectedItem;

        private HomeArticulosVM baseVM;
        public ArticuloHistorialOcupacionVM(HomeArticulosVM baseVM, Articulos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Artículo Historial Ocupación";
            }
        }

        public HistorialOcupacion SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistorialOcupacion)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdArticulo > 0)
            {
                HistorialOcupaciones = db.HistorialOcupacion.Where(m => m.FechaEliminacion == null && m.IdArticulo == entity.IdArticulo).ToList();
                Trazabilidad("Maestros", "Artículos", entity.Articulo, "Consulta", "Mantenimiento Artículos Contratos Clientes");
            }
        }
        protected void ModifyData(HistorialOcupacion contrato)
        {
            
        }
    }
}
