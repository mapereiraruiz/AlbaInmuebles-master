using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class AltaInmueblesDatosWebVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;

        private HomeInmueblesVM baseVM;
        private AltaInmueblesVM ficha;
        private string _serviciosconserjeria;
        private string _serviciosvigilancia;
        private string _serviciospersonalmantenimiento;
        private string _serviciosrecepcion;
        private string _serviciospaqueteria;
        private string _instalacionesaireacondicionado;
        private string _instalacionescalefaccion;
        private string _instalacionesascensores;
        private string _instalacionesincendios;
        private string _instalacionescontrolaccesos;
        private string _instalacionescctv;
        private string _instalacionesiluminacion;
        private string _instalacionespci; 
        private string _instalacionesscanner; 
        private string _generaldescripcion;
        private string _generaluso;
        private string _generalsuperficie;
        private string _generalaparcamiento;
        private string _generalnumeroplazas;
        private string _calidadesfalsostechos;
        private string _calidadessuelostecnicos;
        private string _calidadespavimentos;
        private string _calidadesfachada;
        private string _calidadescarpinteria;
        private string _calidadesrevestimientos;
        private string _calidadesaseosadaptados;
        public AltaInmueblesDatosWebVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            ficha = new AltaInmueblesVM(baseVM, entity);
        }
        public string Name
        {
            get
            {
                return "Alta Inmuebles Datos Web";
            }
        }

        public int MaxDato
        {
            get { return 100; }
        }

        public int MaxDescripcion
        {
            get { return 500; }
        }
        public string ServiciosConserjeria
        {
            get { return _serviciosconserjeria; }
            set
            {
                if (_serviciosconserjeria != value)
                {
                    _serviciosconserjeria = value;
                    entity.ServiciosConserjeria = ServiciosConserjeria;
                    RaisePropertyChanged("ServiciosConserjeria");
                }
            }
        }
        public string ServiciosVigilancia24h
        {
            get { return _serviciosvigilancia; }
            set
            {
                if (_serviciosvigilancia != value)
                {
                    _serviciosvigilancia = value;
                    entity.ServiciosVigilancia24h = ServiciosVigilancia24h;
                    RaisePropertyChanged("ServiciosVigilancia24h");
                }
            }
        }
        public string ServiciosPersonalMantenimiento
        {
            get { return _serviciospersonalmantenimiento; }
            set
            {
                if (_serviciospersonalmantenimiento != value)
                {
                    _serviciospersonalmantenimiento = value;
                    entity.ServiciosPersonalMantenimiento = ServiciosPersonalMantenimiento;
                    RaisePropertyChanged("ServiciosPersonalMantenimiento");
                }
            }
        }
        public string ServiciosRecepcion
        {
            get { return _serviciosrecepcion; }
            set
            {
                if (_serviciosrecepcion != value)
                {
                    _serviciosrecepcion = value;
                    entity.ServiciosRecepcion = ServiciosRecepcion;
                    RaisePropertyChanged("ServiciosRecepcion");
                }
            }
        }
        public string ServiciosPaqueteria
        {
            get { return _serviciospaqueteria; }
            set
            {
                if (_serviciospaqueteria != value)
                {
                    _serviciospaqueteria = value;
                    entity.ServiciosPaqueteria = ServiciosPaqueteria;
                    RaisePropertyChanged("ServiciosPaqueteria");
                }
            }
        }
        public string InstalacionesAireAcondicionado
        {
            get { return _instalacionesaireacondicionado; }
            set
            {
                if (_instalacionesaireacondicionado != value)
                {
                    _instalacionesaireacondicionado = value;
                    entity.InstalacionesAireAcondicionado = InstalacionesAireAcondicionado;
                    RaisePropertyChanged("InstalacionesAireAcondicionado");
                }
            }
        }
        public string InstalacionesCalefaccion
        {
            get { return _instalacionescalefaccion; }
            set
            {
                if (_instalacionescalefaccion != value)
                {
                    _instalacionescalefaccion = value;
                    entity.InstalacionesCalefaccion = InstalacionesCalefaccion;
                    RaisePropertyChanged("InstalacionesCalefaccion");
                }
            }
        }
        public string InstalacionesAscensores
        {
            get { return _instalacionesascensores; }
            set
            {
                if (_instalacionesascensores != value)
                {
                    _instalacionesascensores = value;
                    entity.InstalacionesAscensores = InstalacionesAscensores;
                    RaisePropertyChanged("InstalacionesAscensores");
                }
            }
        }

        public string InstalacionesIncendios
        {
            get { return _instalacionesincendios; }
            set
            {
                if (_instalacionesincendios != value)
                {
                    _instalacionesincendios = value;
                    entity.InstalacionesIncendios = InstalacionesIncendios;
                    RaisePropertyChanged("InstalacionesIncendios");
                }
            }
        }
        public string InstalacionesControlAccesos
        {
            get { return _instalacionescontrolaccesos; }
            set
            {
                if (_instalacionescontrolaccesos != value)
                {
                    _instalacionescontrolaccesos = value;
                    entity.InstalacionesControlAccesos = InstalacionesControlAccesos;
                    RaisePropertyChanged("InstalacionesControlAccesos");
                }
            }
        }
        public string InstalacionesCCTV
        {
            get { return _instalacionescctv; }
            set
            {
                if (_instalacionescctv != value)
                {
                    _instalacionescctv = value;
                    entity.InstalacionesCCTV = InstalacionesCCTV;
                    RaisePropertyChanged("InstalacionesCCTV");
                }
            }
        }
        public string InstalacionesIluminacion
        {
            get { return _instalacionesiluminacion; }
            set
            {
                if (_instalacionesiluminacion != value)
                {
                    _instalacionesiluminacion = value;
                    entity.InstalacionesIluminacion = InstalacionesIluminacion;
                    RaisePropertyChanged("InstalacionesIluminacion");
                }
            }
        }
        public string InstalacionesPCI
        {
            get { return _instalacionespci; }
            set
            {
                if (_instalacionespci != value)
                {
                    _instalacionespci = value;
                    entity.InstalacionesPCI = InstalacionesPCI;
                    RaisePropertyChanged("InstalacionesPCI");
                }
            }
        }
        public string InstalacionesScanner
        {
            get { return _instalacionesscanner; }
            set
            {
                if (_instalacionesscanner != value)
                {
                    _instalacionesscanner = value;
                    entity.InstalacionesScanner = InstalacionesScanner;
                    RaisePropertyChanged("InstalacionesScanner");
                }
            }
        }
        public string GeneralDescripcion
        {
            get { return _generaldescripcion; }
            set
            {
                if (_generaldescripcion != value)
                {
                    _generaldescripcion = value;
                    entity.GeneralDescripcion = GeneralDescripcion;
                    RaisePropertyChanged("GeneralDescripcion");
                }
            }
        }
        public string GeneralUso
        {
            get { return _generaluso; }
            set
            {
                if (_generaluso != value)
                {
                    _generaluso = value;
                    entity.GeneralUso = GeneralUso;
                    RaisePropertyChanged("GeneralUso");
                }
            }
        }
        public string GeneralSuperficie
        {
            get { return _generalsuperficie; }
            set
            {
                if (_generalsuperficie != value)
                {
                    _generalsuperficie = value;
                    entity.GeneralSuperficie = GeneralSuperficie;
                    RaisePropertyChanged("GeneralSuperficie");
                }
            }
        }
        public string GeneralAparcamiento
        {
            get { return _generalaparcamiento; }
            set
            {
                if (_generalaparcamiento != value)
                {
                    _generalaparcamiento = value;
                    entity.GeneralAparcamiento = GeneralAparcamiento;
                    RaisePropertyChanged("GeneralAparcamiento");
                }
            }
        }
        public string GeneralNumeroPlazas
        {
            get { return _generalnumeroplazas; }
            set
            {
                if (_generalnumeroplazas != value)
                {
                    _generalnumeroplazas = value;
                    entity.GeneralNumeroPlazas = GeneralNumeroPlazas;
                    RaisePropertyChanged("GeneralNumeroPlazas");
                }
            }
        }
        public string CalidadesFalsosTechos
        {
            get { return _calidadesfalsostechos; }
            set
            {
                if (_calidadesfalsostechos != value)
                {
                    _calidadesfalsostechos = value;
                    entity.CalidadesFalsosTechos = CalidadesFalsosTechos;
                    RaisePropertyChanged("CalidadesFalsosTechos");
                }
            }
        }
        public string CalidadesSuelosTecnicos
        {
            get { return _calidadessuelostecnicos; }
            set
            {
                if (_calidadessuelostecnicos != value)
                {
                    _calidadessuelostecnicos = value;
                    entity.CalidadesSuelosTecnicos = CalidadesSuelosTecnicos;
                    RaisePropertyChanged("CalidadesSuelosTecnicos");
                }
            }
        }
        public string CalidadesPavimentos
        {
            get { return _calidadespavimentos; }
            set
            {
                if (_calidadespavimentos != value)
                {
                    _calidadespavimentos = value;
                    entity.CalidadesPavimentos = CalidadesPavimentos;
                    RaisePropertyChanged("CalidadesPavimentos");
                }
            }
        }
        public string CalidadesFachada
        {
            get { return _calidadesfachada; }
            set
            {
                if (_calidadesfachada != value)
                {
                    _calidadesfachada = value;
                    entity.CalidadesFachada = CalidadesFachada;
                    RaisePropertyChanged("CalidadesFachada");
                }
            }
        }
        public string CalidadesCarpinterias
        {
            get { return _calidadescarpinteria; }
            set
            {
                if (_calidadescarpinteria != value)
                {
                    _calidadescarpinteria = value;
                    entity.CalidadesCarpinterias = CalidadesCarpinterias;
                    RaisePropertyChanged("CalidadesCarpinterias");
                }
            }
        }
        public string CalidadesRevestimientos
        {
            get { return _calidadesrevestimientos; }
            set
            {
                if (_calidadesrevestimientos != value)
                {
                    _calidadesrevestimientos = value;
                    entity.CalidadesRevestimientos = CalidadesRevestimientos;
                    RaisePropertyChanged("CalidadesRevestimientos");
                }
            }
        }
        public string CalidadesAseosAdaptados
        {
            get { return _calidadesaseosadaptados; }
            set
            {
                if (_calidadesaseosadaptados != value)
                {
                    _calidadesaseosadaptados = value;
                    entity.CalidadesAseosAdaptados = CalidadesAseosAdaptados;
                    RaisePropertyChanged("CalidadesAseosAdaptados");
                }
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity != null)
            {
                ServiciosConserjeria = entity.ServiciosConserjeria;
                ServiciosVigilancia24h = entity.ServiciosVigilancia24h;
                ServiciosPersonalMantenimiento = entity.ServiciosPersonalMantenimiento;
                ServiciosRecepcion = entity.ServiciosRecepcion;
                ServiciosPaqueteria = entity.ServiciosPaqueteria;
                InstalacionesAireAcondicionado = entity.InstalacionesAireAcondicionado;
                InstalacionesCalefaccion = entity.InstalacionesCalefaccion;
                InstalacionesAscensores = entity.InstalacionesAscensores;
                InstalacionesIncendios = entity.InstalacionesIncendios;
                InstalacionesControlAccesos = entity.InstalacionesControlAccesos;
                InstalacionesCCTV = entity.InstalacionesCCTV;
                InstalacionesIluminacion = entity.InstalacionesIluminacion;
                InstalacionesPCI = entity.InstalacionesPCI;
                InstalacionesScanner = entity.InstalacionesScanner;
                GeneralDescripcion = entity.GeneralDescripcion;
                GeneralUso = entity.GeneralUso;
                GeneralSuperficie = entity.GeneralSuperficie;
                GeneralAparcamiento = entity.GeneralAparcamiento;
                GeneralNumeroPlazas = entity.GeneralNumeroPlazas;
                CalidadesFalsosTechos = entity.CalidadesFalsosTechos;
                CalidadesSuelosTecnicos = entity.CalidadesSuelosTecnicos;
                CalidadesPavimentos = entity.CalidadesPavimentos;
                CalidadesFachada = entity.CalidadesFachada;
                CalidadesCarpinterias = entity.CalidadesCarpinterias;
                CalidadesRevestimientos = entity.CalidadesRevestimientos;
                CalidadesAseosAdaptados = entity.CalidadesAseosAdaptados;
            }
        }
    }
}
