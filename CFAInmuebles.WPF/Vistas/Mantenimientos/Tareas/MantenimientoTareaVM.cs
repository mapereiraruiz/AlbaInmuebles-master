using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoTareaVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeTareaVM baseVM;
        private Tareas _selectedItem;
        private string _tarea;
        private string _descripcion;
        
        public  string Name
        {
            get
            {
                return "Mantenimiento Tarea";
            }
        }

        public MantenimientoTareaVM(HomeTareaVM baseVM)
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


        public new string Tarea
        {
            get { return _tarea; }
            set
            {
                if (_tarea != value)
                {
                    _tarea = value;
                    RaisePropertyChanged("Tarea");
                }
            }
        }

        public Tareas SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Tareas)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.ToList();
            TipoFicheros.Insert(0, new TipoFichero { Valor = "Seleccione:" });
            SearchData();
            Trazabilidad("Mantenimientos", "Tareas", "", "Consulta", "Mantenimiento Tareas");
        }

        protected void ModifyData(Tareas entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tarea").FirstOrDefault();
            viewmodel  = new FichaTareaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Tareas", "", "Búsqueda", "Cadena de consulta: Tarea=" + Tarea +                                                                                        
                                                                                        "&Tipo Fichero=" +  TipoFichero?.Valor);

            var search = db.Tareas.Where(m => m.FechaEliminacion == null).AsQueryable();

            if (!String.IsNullOrEmpty(Tarea))
                search = search.Where(m => m.Tarea.Contains(Tarea));

            if (TipoFichero != null && TipoFichero.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoFicheroNavigation == TipoFichero);
            }
            Tareas = search.ToList();
        }
    }
}
