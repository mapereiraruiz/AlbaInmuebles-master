using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ObraLicenciasVM : ObservableViewModel, IPageViewModel
    {
        public HistorialObra entity;

        protected new ICommand _modifyCommand;

        private Licencias _selectedItem;

        private FichaObraVM baseVM;
        public ObraLicenciasVM(FichaObraVM baseVM, HistorialObra entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
    
        }
        public string Name
        {
            get
            {
                return "Obra Licencias";
            }
        }


        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData((Licencias)p));
                }
                return _modifyCommand;
            }
        }

        public Licencias SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdHistorialObra > 0)
            {
                //***var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
                var historialobra = db.HistorialObra.Where(m => m.FechaEliminacion == null && m.IdHistorialObra == entity.IdHistorialObra).Select(m => m.IdHistorialObra).ToList();

                Licencias = db.Licencias.Where(m => m.FechaEliminacion == null && historialobra.Contains(m.IdFichero)).ToList();
                Trazabilidad("Mantenimientos", "Obras", entity.IdHistorialObra.ToString(), "Consulta", "Mantenimiento Obra Licencias");
            }
        }

        protected void ModifyData(Licencias entity)
        {
            HomeLicencia ventana = new HomeLicencia();

            HomeLicenciaVM datacontext = new HomeLicenciaVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaLicenciaVM(datacontext, entity);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}