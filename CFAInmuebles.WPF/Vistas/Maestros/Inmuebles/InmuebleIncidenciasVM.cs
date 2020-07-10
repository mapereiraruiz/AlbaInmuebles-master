using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleIncidenciasVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;
        protected new ICommand _modifyCommand;

        private Incidencias _selectedItem;
        private HomeInmueblesVM baseVM;
        public InmuebleIncidenciasVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Incidencias";
            }
        }

        public Incidencias SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Incidencias)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdInmueble > 0)
            {
                Incidencias = db.Incidencias.Where(m => m.FechaEliminacion == null && m.IdTipoFicheroNavigation.Valor == "Empresa" && m.IdFichero == entity.IdEmpresa).ToList();

                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Incidencias");
            }
        }

        protected void ModifyData(Incidencias incidencia)
        {
            HomeIncidencias ventana = new HomeIncidencias();

            HomeIncidenciasVM datacontext = new HomeIncidenciasVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaIncidenciasVM(datacontext, incidencia);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
