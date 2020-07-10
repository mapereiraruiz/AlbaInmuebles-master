using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoGastosVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeGastosVM baseVM;
        private Gastos _selectedItem;
        private string _gasto;
        private string _cuentacontable;
        private int? _ivaS;

        public string Name
        {
            get
            {
                return "Mantenimiento Gasto";
            }
        }

        public MantenimientoGastosVM(HomeGastosVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public new string Gasto
        {
            get { return _gasto; }
            set
            {
                if (_gasto != value)
                {
                    _gasto = value;
                    RaisePropertyChanged("Gasto");
                }
            }
        }

        public string CuentaContable
        {
            get { return _cuentacontable; }
            set
            {
                if (_cuentacontable != value)
                {
                    _cuentacontable = value;
                    RaisePropertyChanged("CuentaContable");
                }
            }
        }

        public int? IvaS
        {
            get { return _ivaS; }
            set
            {
                if (_ivaS != value)
                {
                    _ivaS = value;
                    RaisePropertyChanged("IvaS");
                }
            }
        }

        public Gastos SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Gastos)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TiposGasto = db.TipoGasto.ToList();
            TiposGasto.Insert(0, new TipoGasto { Valor = "Seleccione:" });

            Gastos = db.Gastos.Where(m => m.FechaEliminacion == null).ToList();
            SearchData();
            Trazabilidad("Mantenimientos", "Gastos", "", "Consulta", "Mantenimiento Gasto");
        }

        protected void ModifyData(Gastos entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Gasto").FirstOrDefault();
            viewmodel = new FichaGastosVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Mantenimientos", "Gastos", "", "Búsqueda", "Cadena de consulta: Gasto=" + Gasto + "&Tipo Gasto=" + TipoGasto?.Valor
                + "&Iva=" + IvaS);

            var search = db.Gastos.Where(m => m.FechaEliminacion == null).AsQueryable();

            if (!String.IsNullOrEmpty(Gasto))
                search = search.Where(m => m.Gasto.Contains(Gasto));

            if (TipoGasto != null && TipoGasto.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoGastoNavigation == TipoGasto);
            }

            Gastos = search.ToList();
        }
    }
}

