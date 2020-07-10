using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoProvinciasVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeProvinciasVM baseVM;
        private Provincias _selectedItem;
        private string _provincia;
        private bool? _conveniofianza;
        private double? _porcentajedeposito;
        private string _numeroconcierto;
        public string Name
        {
            get
            {
                return "Mantenimiento Provincias";
            }
        }

        public MantenimientoProvinciasVM(HomeProvinciasVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public new string Provincia
        {
            get { return _provincia; }
            set
            {
                if (_provincia != value)
                {
                    _provincia = value;
                    RaisePropertyChanged("Provincia");
                }
            }
        }

        public bool? Conveniofianza
        {
            get { return _conveniofianza; }
            set
            {
                if (_conveniofianza != value)
                {
                    _conveniofianza = value;
                    RaisePropertyChanged("Conveniofianza");
                }
            }
        }

        public double? PorcentajeDeposito
        {
            get { return _porcentajedeposito; }
            set
            {
                if (_porcentajedeposito != value)
                {
                    _porcentajedeposito = value;
                    RaisePropertyChanged("PorcentajeDeposito");
                }
            }
        }

        public string NumeroConcierto
        {
            get { return _numeroconcierto; }
            set
            {
                if (_numeroconcierto != value)
                {
                    _numeroconcierto = value;
                    RaisePropertyChanged("NumeroConcierto");
                }
            }
        }


        public Provincias SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Provincias)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();        
            SearchData();
            Trazabilidad("Mantenimientos", "Provincias", "", "Consulta", "Mantenimiento Provincia");
        }

        protected void ModifyData(Provincias entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Provincias").FirstOrDefault();
            viewmodel = new FichaProvinciasVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Provincias", "", "Búsqueda", "Cadena de consulta: Provincia=" + Provincia);

            var search = db.Provincias.Where(m => m.FechaEliminacion == null).AsQueryable();

            if (!String.IsNullOrEmpty(Provincia))
                search = search.Where(m => m.Provincia.Contains(Provincia));

            Provincias = search.ToList();
        }
    }
}


