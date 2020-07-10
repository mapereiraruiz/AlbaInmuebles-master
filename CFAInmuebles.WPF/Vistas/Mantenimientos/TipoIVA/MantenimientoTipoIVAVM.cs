using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoTipoIVAVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeTipoIVAVM baseVM;
        private TipoIva _selectedItem;
        private string _tipoIva1;
        private string _cuentacontable;
        private int? _ivaS;

        public string Name
        {
            get
            {
                return "Mantenimiento Tipo IVA";
            }
        }

        public MantenimientoTipoIVAVM(HomeTipoIVAVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string TipoIva1
        {
            get { return _tipoIva1; }
            set
            {
                if (_tipoIva1 != value)
                {
                    _tipoIva1 = value;
                    RaisePropertyChanged("TipoIva1");
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

        public TipoIva SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((TipoIva)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            Empresas = db.Empresas.ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });

            SearchData();
            Trazabilidad("Mantenimientos", "Tipos IVA", "", "Consulta", "Mantenimiento Tipo IVA");
        }

        protected void ModifyData(TipoIva entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo IVA").FirstOrDefault();
            viewmodel = new FichaTipoIVAVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Mantenimientos", "Tipos IVA", "", "Búsqueda", "Cadena de consulta: Tipo Iva=" + TipoIva1 + "&Cuenta Contable=" + CuentaContable
                + "&Iva=" + IvaS);

            var search = db.TipoIva.Where(m => m.FechaEliminacion == null).AsQueryable();

            if (!String.IsNullOrEmpty(TipoIva1))
                search = search.Where(m => m.TipoIva1.Contains(TipoIva1));

            if (!String.IsNullOrEmpty(CuentaContable))
                search = search.Where(m => m.CuentaContable.Contains(CuentaContable));

            if (IvaS != null) 
                search = search.Where(m => m.Iva == IvaS);

            TipoIvas = search.ToList();
        }
    }
}

