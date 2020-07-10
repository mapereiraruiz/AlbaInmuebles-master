using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ArticuloObrasVM : ObservableViewModel, IPageViewModel
    {
        public Articulos entity;

        private HistorialObra _selectedItem;

        protected new ICommand _modifyCommand;

        private FichaArticulosVM baseVM;
        public ArticuloObrasVM(FichaArticulosVM baseVM, Articulos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
    
        }
        public string Name
        {
            get
            {
                return "Artículo Obras";
            }
        }

        public HistorialObra SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistorialObra)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdArticulo > 0)
            {
                var ficheroobras = db.ObrasFichero.Where(m => m.IdFichero == entity.IdArticulo && m.IdTipoFicheroNavigation.Valor == "Artículo").Select(m => m.IdHistorialObra).ToList();
                Obras = db.HistorialObra.Where(m => m.FechaEliminacion == null && ficheroobras.Contains(m.IdHistorialObra)).ToList();           
                Trazabilidad("Maestros", "Artículos", entity.Articulo, "Consulta", "Mantenimiento Artículos Obras");
            }
        }

        protected void ModifyData(HistorialObra entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Artículo Obras").FirstOrDefault();
            viewmodel = new AltaArticuloObrasVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}