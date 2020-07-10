using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoConceptoFacturacionVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeConceptoFacturacionVM baseVM;
        private ConceptoFacturacion _selectedItem;
        private string _conceptofacturacion;
        private bool? _revisableipc;

        public string Name
        {
            get
            {
                return "Mantenimiento Concepto de Facturación";
            }
        }

        public MantenimientoConceptoFacturacionVM(HomeConceptoFacturacionVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string Conceptofacturacion1
        {
            get { return _conceptofacturacion; }
            set
            {
                if (_conceptofacturacion != value)
                {
                    _conceptofacturacion = value;
                    RaisePropertyChanged("Conceptofacturacion1");
                }
            }
        }
        public bool? RevisableIpc
        {
            get { return _revisableipc; }
            set
            {
                if (_revisableipc != value)
                {
                    _revisableipc = value;
                    RaisePropertyChanged("RevisableIpc");
                }
            }
        }     
        public ConceptoFacturacion SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((ConceptoFacturacion)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoIvas = db.TipoIva.ToList();
            TipoIvas.Insert(0, new TipoIva { TipoIva1 = "Seleccione:" });

            CatalogarConceptos = db.CatalogarConcepto.ToList();
            CatalogarConceptos.Insert(0, new CatalogarConcepto { Valor = "Seleccione:" });

            ConceptosFacturacion = db.ConceptoFacturacion.Where(m => m.FechaEliminacion == null).ToList();

            SearchData();

            Trazabilidad("Mantenimientos", "Concepto Facturación", "", "Consulta", "Mantenimiento Concepto Facturación");
        }

        protected void ModifyData(ConceptoFacturacion entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Concepto de Facturación").FirstOrDefault();
            viewmodel = new FichaConceptoFacturacionVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            var search = db.ConceptoFacturacion.Where(m => m.FechaEliminacion == null).AsQueryable();
            Trazabilidad("Mantenimientos", "Concepto Facturación", "", "Búsqueda", "Cadena de consulta: Concepto Facturación=" + Conceptofacturacion1 + 
                "&Revisable=" + RevisableIpc + "&Tipo Iva=" + TipoIva + "&Catalogar Concepto=" + CatalogarConcepto?.Valor);

            if (!String.IsNullOrEmpty(Conceptofacturacion1))
                search = search.Where(m => m.Conceptofacturacion1.Contains(Conceptofacturacion1));

            if (RevisableIpc != null)
                search = search.Where(m => m.RevisableIpc == RevisableIpc);

            if (TipoIva != null && TipoIva.TipoIva1 != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoIvaNavigation == TipoIva);
            }

            if (CatalogarConcepto != null && CatalogarConcepto.Valor != "Seleccione:")
                search = search.Where(m => m.IdCatalogarConceptoNavigation == CatalogarConcepto);


            ConceptosFacturacion = search.ToList();
        }
    }
}
