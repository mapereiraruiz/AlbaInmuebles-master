using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaInmuebleReferenciaCatastralVM : ObservableViewModel, IPageViewModel
    {
        public ReferenciasCatastrales entity;
        public Inmuebles entitybase;

        private FichaInmueblesVM baseVM;
        private string _fichero;
        private string _descripcion;
        private DateTime? _fechaapertura;
        private DateTime? _fechacierre;
        private string _numeroexpediente;
        private decimal? _presupuestoadjudicacion;
        private decimal? _costefinal;
        private decimal? _costeasumido;
        private bool _direccionfacultativaSINO;
        private string _tipo;
        private string _direccionfacultativa;



        private bool _selectedItem;

        public AltaInmuebleReferenciaCatastralVM(FichaInmueblesVM baseVM, Inmuebles entitybase, ReferenciasCatastrales entity = null)
        {
            this.entity = entity;
            this.entitybase = entitybase;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Inmueble Referencia Catastral";
            }
        }

        public string Fichero
        {
            get { return _fichero; }
            set
            {
                if (_fichero != value)
                {
                    _fichero = value;
                    RaisePropertyChanged("Fichero");
                }
            }
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

        public string NumeroExpediente
        {
            get { return _numeroexpediente; }
            set
            {
                if (_numeroexpediente != value)
                {
                    _numeroexpediente = value;
                    RaisePropertyChanged("NumeroExpediente");
                }
            }
        }

        public DateTime? FechaApertura
        {
            get { return _fechaapertura; }
            set
            {
                if (_fechaapertura != value)
                {
                    _fechaapertura = value;
                    RaisePropertyChanged("FechaApertura");
                }
            }
        }

        public DateTime? FechaCierre
        {
            get { return _fechacierre; }
            set
            {
                if (_fechacierre != value)
                {
                    _fechacierre = value;
                    RaisePropertyChanged("FechaCierre");
                }
            }
        }

        public decimal? PresupuestoAdjudicacion
        {
            get { return _presupuestoadjudicacion; }
            set
            {
                if (_presupuestoadjudicacion != value)
                {
                    _presupuestoadjudicacion = value;
                    RaisePropertyChanged("PresupuestoAdjudicacion");
                }
            }
        }

        public decimal? CosteFinal
        {
            get { return _costefinal; }
            set
            {
                if (_costefinal != value)
                {
                    _costefinal = value;
                    RaisePropertyChanged("CosteFinal");
                }
            }
        }

        public decimal? CosteAsumido
        {
            get { return _costeasumido; }
            set
            {
                if (_costeasumido != value)
                {
                    _costeasumido = value;
                    RaisePropertyChanged("CosteAsumido");
                }
            }
        }

        public string Tipo
        {
            get { return _tipo; }
            set
            {
                if (_tipo != value)
                {
                    _tipo = value;
                    RaisePropertyChanged("Tipo");
                }
            }
        }

        public bool DireccionFacultativaSino
        {
            get { return _direccionfacultativaSINO; }
            set
            {
                if (_direccionfacultativaSINO != value)
                {
                    _direccionfacultativaSINO = value;
                    RaisePropertyChanged("DireccionFacultativaSino");
                }
            }
        }

        public string DireccionFacultativa
        {
            get { return _direccionfacultativa; }
            set
            {
                if (_direccionfacultativa != value)
                {
                    _direccionfacultativa = value;
                    RaisePropertyChanged("DireccionFacultativa");
                }
            }
        }




        public Visibility MostrarBotones
        {
            get
            {
                if (entity == null)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        public bool SelectedItem
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
            TipoFicheros = db.TipoFichero.ToList();

           // if (entitybase != null)
             //   Proveedores = db.Proveedor.Where(m => m.FechaEliminacion == null && m.Cli == entitybase.IdEmpresa).ToList();
        }

        protected override void ModifyData()
        {
            base.ModifyData();

            var model = db.ReferenciasCatastrales.Find(entity?.IdReferenciaCatastral);

            if (model == null && entity != null)
                db.ReferenciasCatastrales.Add(entity);

            db.SaveChanges();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Inmueble Obras").FirstOrDefault());
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Inmueble Referencia Catastral").FirstOrDefault();
            viewmodel = new InmuebleReferenciaCatastralVM(baseVM, entitybase);
            baseVM.CurrentPageViewModel = viewmodel;
       
        }
    }
}
