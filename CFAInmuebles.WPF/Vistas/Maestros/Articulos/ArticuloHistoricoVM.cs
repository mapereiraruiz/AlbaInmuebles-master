using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ArticuloHistoricoVM : ObservableViewModel, IPageViewModel
    {
        public Articulos entity;

        protected new ICommand _modifyCommand;
       

        private FichaArticulosVM baseVM;
        private HistoricoArticulos _selectedItem;
        public ArticuloHistoricoVM(FichaArticulosVM baseVM, Articulos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Artículo Histórico Modificaciones";
            }
        }

        public HistoricoArticulos SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistoricoArticulos)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdArticulo > 0)
            {
                HistoricoArticulos = db.HistoricoArticulos.Where(m => m.FechaEliminacion == null &&  m.IdArticulo == entity.IdArticulo).OrderByDescending(m => m.IdHistoricoArticulo).ToList();

                Trazabilidad("Maestros", "Artículos", entity.Articulo, "Consulta", "Mantenimiento Artículos Histórico Modificaciones");
            }
        }

        protected void ModifyData(HistoricoArticulos historico)
        {
            //var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Artículo Histórico").FirstOrDefault();
            //viewmodel = new FichaArticuloHistoricoVM(baseVM, this.entity, historico);
            //baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
