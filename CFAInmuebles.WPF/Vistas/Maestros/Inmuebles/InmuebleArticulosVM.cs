using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleArticulosVM : ObservableViewModel, IPageViewModel 
    {
        public Inmuebles entity;

        protected new ICommand _modifyCommand;
        
        private Articulos _selectedItem;
        private HomeInmueblesVM baseVM;

       
        public InmuebleArticulosVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Artículos";
            }
        }

        public Articulos SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Articulos)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdInmueble > 0)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
                Articulos = db.Articulos.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Artículos");
            }
        }

        protected void ModifyData(Articulos articulo)
        {
            HomeArticulos ventana = new HomeArticulos();

            HomeArticulosVM datacontext = new HomeArticulosVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaArticulosVM(datacontext, articulo);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
