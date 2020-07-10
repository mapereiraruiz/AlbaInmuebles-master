using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoFormasDePagoVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeFormasDePagoVM baseVM;
        private FormasDePago _selectedItem;
        private string _formapago;
        private int? _codigoformapagos;

        public string Name
        {
            get
            {
                return "Mantenimiento Formas De Pago";
            }
        }

        public MantenimientoFormasDePagoVM(HomeFormasDePagoVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string FormaPago
        {
            get { return _formapago; }
            set
            {
                if (_formapago != value)
                {
                    _formapago = value;
                    RaisePropertyChanged("FormaPago");
                }
            }
        }

        public int? CodigoFormaPagoS
        {
            get { return _codigoformapagos; }
            set
            {
                if (_codigoformapagos != value)
                {
                    _codigoformapagos = value;
                    RaisePropertyChanged("CodigoFormaPagoS");
                }
            }
        }

        public FormasDePago SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((FormasDePago)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            FormasDePago = db.FormasDePago.Where(m => m.FechaEliminacion == null).ToList();
            SearchData();
            Trazabilidad("Mantenimientos", "Formas de Pago", "", "Consulta", "Mantenimiento Formas de Pago");
        }

        protected void ModifyData(FormasDePago entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Formas De Pago").FirstOrDefault();
            viewmodel = new FichaFormasDePagoVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Formas de Pago", "", "Búsqueda", "Cadena de consulta: Forma Pago=" + FormaPago + "&Código=" + CodigoFormaPagoS?.ToString());

            var search = db.FormasDePago.Where(m => m.FechaEliminacion == null).AsQueryable();

            if (!String.IsNullOrEmpty(FormaPago))
                search = search.Where(m => m.FormaPago.Contains(FormaPago));

            if (CodigoFormaPagoS != null)
                search = search.Where(m => m.CodigoFormaPago.ToString().Contains(CodigoFormaPagoS.ToString()));

            FormasDePago = search.ToList();
        }
    }
}

