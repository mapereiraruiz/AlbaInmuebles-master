using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoTipoArticuloVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeTipoArticuloVM baseVM;
        private TipoArticulos _selectedItem;
        private string _tipoArticulo;
        private bool? _alquilable;

        public string Name
        {
            get
            {
                return "Mantenimiento Tipo Artículo";
            }
        }

        public MantenimientoTipoArticuloVM(HomeTipoArticuloVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public new string TipoArticulo
        {
            get { return _tipoArticulo; }
            set
            {
                if (_tipoArticulo != value)
                {
                    _tipoArticulo = value;
                    RaisePropertyChanged("TipoArticulo");
                }
            }
        }
        public bool? Alquilable
        {
            get { return _alquilable; }
            set
            {
                if (_alquilable != value)
                {
                    _alquilable = value;
                    RaisePropertyChanged("Alquilable");
                }
            }
        }
        public TipoArticulos SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((TipoArticulos)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoMedidas = db.TipoMedida.ToList();
            TipoMedidas.Insert(0, new TipoMedida { Valor = "Seleccione:" });

            TipoConceptoArticulos = db.TipoConceptoArticulo.ToList();
            TipoConceptoArticulos.Insert(0, new TipoConceptoArticulo { Valor = "Seleccione:" });
            
            Clasificaciones = db.Clasificacion.ToList();
            Clasificaciones.Insert(0, new Clasificacion { Valor = "Seleccione:" });

            SearchData();
            Trazabilidad("Mantenimientos", "Tipo Artículo", "", "Consulta", "Mantenimiento Tipo Artículo");
        }
         
        protected void ModifyData(TipoArticulos entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tipo Artículo").FirstOrDefault();
            viewmodel = new FichaTipoArticuloVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Mantenimientos", "Tipo Artículo", "", "Búsqueda", "Cadena de consulta: Tipo Artículo=" + TipoArticulo
                + "&Tipo Medida=" + TipoMedida?.Valor + "&Tipo Concepto Artículo=" + TipoConceptoArticulo?.Valor + "&Clasificación=" + Clasificacion?.Valor);
            var search = db.TipoArticulos.Where(m => m.FechaEliminacion == null).AsQueryable();

            if (!String.IsNullOrEmpty(TipoArticulo))
                search = search.Where(m => m.Tipoarticulo.Contains(TipoArticulo));

            if (TipoMedida != null && TipoMedida.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoMedidaNavigation == TipoMedida);
            }

            if (TipoConceptoArticulo != null && TipoConceptoArticulo.Valor != "Seleccione:")
                search = search.Where(m => m.IdTipoConceptoArticuloNavigation == TipoConceptoArticulo);

            if (Clasificacion != null && Clasificacion.Valor != "Seleccione:")
                search = search.Where(m => m.IdClasificacionNavigation == Clasificacion);

            if (Alquilable != null)
                search = search.Where(m => m.Alquilable == Alquilable);

            TipoArticulos = search.ToList();
        }
    }
}
