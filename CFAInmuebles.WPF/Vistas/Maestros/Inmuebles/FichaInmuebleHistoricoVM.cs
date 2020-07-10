using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class FichaInmuebleHistoricoVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public HistoricoInmuebles entity;
		public Inmuebles entitybase;

		private FichaInmueblesVM baseVM;

        private string _inmueble;
        private Empresas _empresa;
        private TipoInmuebles _tipoinmueble;
        private Provincias _provincia;
        private string _calle;
        private string _municipio;
        private string _codigopostal;
        private string _referenciacastastralprincipal;
        private string _superficieparcela;
        private bool _usopropio;
        private string _superficieregistrals;
        private string _superficieregistralb;
        private string _superficiecatastrals;
        private string _superficiecatastralb;
        private string _superficiealbas;
        private string _superficiealbab;
        private string _numplazaregistral;
        private string _numplazacatastral;
        private string _numplazaalba;

        private bool _selectedItem;

		public FichaInmuebleHistoricoVM(FichaInmueblesVM baseVM, Inmuebles entitybase, HistoricoInmuebles entity = null)
		{
            this.entity = entity ?? new HistoricoInmuebles();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}


		public string Name
        {
            get
            {
                 return "Ficha Inmueble Histórico";
            }
        }

        public new string Inmueble
        {
            get
            {
                return _inmueble;
            }
            set
            {
                if (_inmueble != value)
                {
                    _inmueble = value;
                    RaisePropertyChanged("Inmueble");
                }
            }
        }

        public new Empresas Empresa
        {
            get
            {
                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                    RaisePropertyChanged("Empresa");
                }
            }
        }
        public new TipoInmuebles TipoInmueble
        {
            get
            {
                return _tipoinmueble;
            }
            set
            {
                if (_tipoinmueble != value)
                {
                    _tipoinmueble = value;                  
                    RaisePropertyChanged("TipoInmueble");
                }
            }
        }

        public new Provincias Provincia
        {
            get
            {
                return _provincia;
            }
            set
            {
                if (_provincia != value)
                {
                    _provincia = value;
                    RaisePropertyChanged("Provincia");
                }
            }
        }


        public string Calle
        {
            get
            {
                return _calle;
            }
            set
            {
                if (_calle != value)
                {
                    _calle = value;
                    RaisePropertyChanged("Calle");
                }
            }
        }

        public string Municipio
        {
            get
            {
                return _municipio;
            }
            set
            {
                if (_municipio != value)
                {
                    _municipio = value;
                    RaisePropertyChanged("Municipio");
                }
            }
        }

        public string CodigoPostal
        {
            get { return _codigopostal; }
            set
            {
                if (_codigopostal != value)
                {
                    _codigopostal = value;
                    RaisePropertyChanged("CodigoPostal");
                }
            }
        }

        public string ReferenciaCatastralPrincipal
        {
            get { return _referenciacastastralprincipal; }
            set
            {
                if (_referenciacastastralprincipal != value)
                {
                    _referenciacastastralprincipal = value;
                    RaisePropertyChanged("ReferenciaCatastralPrincipal");
                }
            }
        }

        public string SuperficieParcela
        {
            get
            {
                return _superficieparcela;
            }
            set
            {
                if (_superficieparcela != value)
                {
                    _superficieparcela = value;
                    RaisePropertyChanged("SuperficieParcela");
                }
            }
        }

        public bool UsoPropio
        {
            get { return _usopropio; }
            set
            {
                if (_usopropio != value)
                {
                    _usopropio = value;
                    RaisePropertyChanged("UsoPropio");
                }
            }
        }

        public string SuperficieRegistralS
        {
            get
            {
                return _superficieregistrals;
            }
            set
            {
                if (_superficieregistrals != value)
                {
                    _superficieregistrals = value;
                    RaisePropertyChanged("SuperficieRegistralS");
                }
            }
        }

        public string SuperficieRegistralB
        {
            get
            {
                return _superficieregistralb;
            }
            set
            {
                if (_superficieregistralb != value)
                {
                    _superficieregistralb = value;
                    RaisePropertyChanged("SuperficieRegistralB");
                }
            }
        }

        public string SuperficieAlbaS
        {
            get
            {
                return _superficiealbas;
            }
            set
            {
                if (_superficiealbas != value)
                {
                    _superficiealbas = value;
                    RaisePropertyChanged("SuperficieAlbaS");
                }
            }
        }


        public string SuperficieAlbaB
        {
            get
            {
                return _superficiealbab;
            }
            set
            {
                if (_superficiealbab != value)
                {
                    _superficiealbab = value;
                    RaisePropertyChanged("SuperficieAlbaB");
                }
            }
        }


        public string SuperficieCatastralS
        {
            get
            {
                return _superficiecatastrals;
            }
            set
            {
                if (_superficiecatastrals != value)
                {
                    _superficiecatastrals = value;
                    RaisePropertyChanged("SuperficieCatastralS");
                }
            }
        }


        public string SuperficieCatastralB
        {
            get
            {
                return _superficiecatastralb;
            }
            set
            {
                if (_superficiecatastralb != value)
                {
                    _superficiecatastralb = value;
                    RaisePropertyChanged("SuperficieCatastralB");
                }
            }
        }

        public string NumPlazaRegistral
        {
            get
            {
                return _numplazaregistral;
            }
            set
            {
                if (_numplazaregistral != value)
                {
                    _numplazaregistral = value;
                    RaisePropertyChanged("NumPlazaRegistral");
                }
            }
        }


        public string NumPlazaCatastral
        {
            get
            {
                return _numplazacatastral;
            }
            set
            {
                if (_numplazacatastral != value)
                {
                    _numplazacatastral = value;
                    RaisePropertyChanged("NumPlazaCatastral");
                }
            }
        }


        public string NumPlazaAlba
        {
            get
            {
                return _numplazaalba;
            }
            set
            {
                if (_numplazaalba != value)
                {
                    _numplazaalba = value;
                    RaisePropertyChanged("NumPlazaAlba");
                }
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

            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            TipoInmuebles = db.TipoInmuebles.Where(m => m.FechaEliminacion == null).ToList();
            Provincias = db.Provincias.Where(m => m.FechaEliminacion == null).ToList();

            if (entity != null)
            {
                Empresa = db.Empresas.Find(entity.IdEmpresa);
                TipoInmueble = db.TipoInmuebles.Find(entity.IdTipoInmueble);
                Provincia = db.Provincias.Find(entity.IdProvincia);
                Inmueble = entity.Inmueble;
                Calle = entity.Calle;
                Municipio = entity.Municipio;
                CodigoPostal = entity.CodigoPostal;
                ReferenciaCatastralPrincipal = entity.ReferenciaCatastralGeneral;
                SuperficieParcela = entity.SuperficieParcela.ToString();
                UsoPropio = entity.UsoPropio;
                SuperficieRegistralS = entity.SuperficieRegistralS.ToString();
                SuperficieRegistralB = entity.SuperficieRegistralB.ToString();
                NumPlazaRegistral = entity.NumPlazaRegistral?.ToString();
                SuperficieCatastralS = entity.SuperficieCatastralS.ToString();
                SuperficieCatastralB = entity.SuperficieCatastralB.ToString();
                NumPlazaCatastral = entity.NumPlazaCatastral?.ToString();
                SuperficieAlbaS = entity.SuperficieAlbaS.ToString();
                SuperficieAlbaB = entity.SuperficieAlbaB.ToString();
                NumPlazaAlba = entity.NumPlazaAlba?.ToString();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Ficha Inmuebles Histórico");
            }
        }


        protected override void VolverListado()
        {
			base.VolverListado();
			var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Inmueble Histórico Modificación").FirstOrDefault();
			viewmodel = new InmuebleHistoricoVM(baseVM, entitybase);
			baseVM.CurrentPageViewModel = viewmodel;
		}
	}
}
