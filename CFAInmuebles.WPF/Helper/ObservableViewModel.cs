using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using CFAInmuebles.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ObservableViewModel : ObservableObject
    {
        protected CFADbContext db;
        public List<CFADbContext> dbsALTAI;
        protected ICommand _loadCommand;
        protected ICommand _deleteCommand;
        protected ICommand _modifyCommand;
        protected ICommand _volverCommand;
        protected ICommand _searchCommand;
        protected ICommand _importCommand;
        protected ICommand _generateCommand;



        protected string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last()?.ToUpper();
        Usuarios user = new Usuarios();
        

        public ObservableViewModel()
        {
            var obj = App.Current as App;
            db = obj.db;
            dbsALTAI = obj.dbsALTAI;
            user = db.Usuarios.Where(m => m.NombreUsuario == userName).FirstOrDefault();  
        }

        protected Usuarios UserId
        {
            get
            {
                if (user != null)
                    return db.Usuarios.Where(m => m.NombreUsuario == user.NombreUsuario).FirstOrDefault();
                else
                    return null;
            }
        }


        // Titulo de la aplicación
        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                if (_titulo != value)
                {
                    _titulo = value;
                    RaisePropertyChanged("Titulo");    
                }
            }
        }

        // Permisos
       
        public Visibility PermisosBloque1
        {
            get
            {
                if (user != null && (user.Administrador || user.PermisosBloque1 == 1))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility PermisosBloque2
        {
            get
            {
                if (user != null && (user.Administrador || user.PermisosBloque2 == 1))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility PermisosBloque3
        {
            get
            {
                if (user != null && (user.Administrador || user.PermisosBloque3 == 1))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility PermisosBloque4
        {
            get
            {
                if (user != null && (user.Administrador || user.PermisosBloque4 == 1))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility PermisosBloque5
        {
            get
            {
                if (user != null && (user.Administrador || user.PermisosBloque5 == 1))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }


        private string mensaje;
        public string Mensaje
        {
            get { return mensaje; }
            set
            {
                if (mensaje != value)
                {
                    mensaje = value;
                    RaisePropertyChanged("Mensaje");
                    RaisePropertyChanged("MostrarMensajes");
                }
            }
        }

        // Entidades

        private Empresas _empresa;
        public Empresas Empresa
        {
            get { return _empresa; }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                    RaisePropertyChanged("Empresa");
                }
            }
        }

       

        private Terceros _tercero;
        public Terceros Tercero
        {
            get { return _tercero; }
            set
            {
                if (_tercero != value)
                {
                    _tercero = value;
                    RaisePropertyChanged("Empresa");
                }
            }
        }

        private HistoricoEmpresas _historicoempresa;
        public HistoricoEmpresas HistoricoEmpresa
        {
            get { return _historicoempresa; }
            set
            {
                if (_historicoempresa != value)
                {
                    _historicoempresa = value;
                    RaisePropertyChanged("HistoricoEmpresa");
                }
            }
        }

        private HistoricoArticulos _historicoarticulo;
        public HistoricoArticulos HistoricoArticulo
        {
            get { return _historicoarticulo; }
            set
            {
                if (_historicoarticulo != value)
                {
                    _historicoarticulo = value;
                    RaisePropertyChanged("HistoricoArticulo");
                }
            }
        }

        private HistoricoContratosClientes _historicocontratocliente;
        public HistoricoContratosClientes HistoricoContratoCliente
        {
            get { return _historicocontratocliente; }
            set
            {
                if (_historicocontratocliente != value)
                {
                    _historicocontratocliente = value;
                    RaisePropertyChanged("HistoricoContratoCliente");
                }
            }
        }

        private HistoricoContratosProveedores _historicocontratoproveedor;
        public HistoricoContratosProveedores HistoricoContratoProveedor
        {
            get { return _historicocontratoproveedor; }
            set
            {
                if (_historicocontratoproveedor != value)
                {
                    _historicocontratoproveedor = value;
                    RaisePropertyChanged("HistoricoContratoProveedor");
                }
            }
        }

        private VentaParcialInmueble _ventaparcialinmueble;
        public VentaParcialInmueble VentaParcialInmueble
        {
            get { return _ventaparcialinmueble; }
            set
            {
                if (_ventaparcialinmueble != value)
                {
                    _ventaparcialinmueble = value;
                    RaisePropertyChanged("VentaParcialInmueble");
                }
            }
        }


        public Logs log;

        private Swift _swift;
        public Swift Swift
        {
            get { return _swift; }
            set
            {
                if (_swift != value)
                {
                    _swift = value;
                    RaisePropertyChanged("Swift");
                }
            }
        }

        private ContratosClientes _contratocliente;
        public ContratosClientes ContratoCliente
        {
            get { return _contratocliente; }
            set
            {
                if (_contratocliente != value)
                {
                    _contratocliente = value;
                    RaisePropertyChanged("ContratoCliente");
                }
            }
        }

        private ModalidadFactura _modalidadfactura;
        public ModalidadFactura ModalidadFactura
        {
            get { return _modalidadfactura; }
            set
            {
                if (_modalidadfactura != value)
                {
                    _modalidadfactura = value;
                    RaisePropertyChanged("ModalidadFactura");
                }
            }
        }


        private ConceptoFacturacion _conceptofacturacion;
        public ConceptoFacturacion ConceptoFacturacion
        {
            get { return _conceptofacturacion; }
            set
            {
                if (_conceptofacturacion != value)
                {
                    _conceptofacturacion = value;
                    RaisePropertyChanged("ConceptoFacturacion");
                }
            }
        }


        private Periodicidad _periodicidad;
        public Periodicidad Periodicidad
        {
            get { return _periodicidad; }
            set
            {
                if (_periodicidad != value)
                {
                    _periodicidad = value;
                    RaisePropertyChanged("Periodicidad");
                }
            }
        }

        private InmuebleCentroCoste _inmueblecentrocoste;
        public InmuebleCentroCoste InmuebleCentroCoste
        {
            get { return _inmueblecentrocoste; }
            set
            {
                if (_inmueblecentrocoste != value)
                {
                    _inmueblecentrocoste = value;
                    RaisePropertyChanged("InmuebleCentroCoste");
                }
            }
        }

        private TipoIpc _tipoipc;
        public TipoIpc TipoIpc
        {
            get { return _tipoipc; }
            set
            {
                if (_tipoipc != value)
                {
                    _tipoipc = value;
                    RaisePropertyChanged("TipoIpc");
                }
            }
        }

        private TipoInmuebles _tipoinmueble;
        public TipoInmuebles TipoInmueble
        {
            get { return _tipoinmueble; }
            set
            {
                if (_tipoinmueble != value)
                {
                    _tipoinmueble = value;
                    RaisePropertyChanged("TipoInmueble");
                }
            }
        }

        private Provincias _provincia;
        public Provincias Provincia
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

        private TipoMedida _tipomedida;
        public TipoMedida TipoMedida
        {
            get { return _tipomedida; }
            set
            {
                if (_tipomedida != value)
                {
                    _tipomedida = value;
                    RaisePropertyChanged("TipoMedida");
                }
            }
        }

        private PeriodicidadServicio _periodicidadservicio;
        public PeriodicidadServicio PeriodicidadServicio
        {
            get { return _periodicidadservicio; }
            set
            {
                if (_periodicidadservicio != value)
                {
                    _periodicidadservicio = value;
                    RaisePropertyChanged("PeriodicidadServicio");
                }
            }
        }



        private Normas _norma;
        public Normas Norma
        {
            get { return _norma; }
            set
            {
                if (_norma != value)
                {
                    _norma = value;
                    RaisePropertyChanged("Norma");
                }
            }
        }


        private Tareas _tarea;
        public Tareas Tarea
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


        private TipoInstalacion _tipoinstalacion;
        public TipoInstalacion TipoInstalacion
        {
            get { return _tipoinstalacion; }
            set
            {
                if (_tipoinstalacion != value)
                {
                    _tipoinstalacion = value;
                    RaisePropertyChanged("TipoInstalacion");
                }
            }
        }

        private TipoIva _tipoiva;
        public TipoIva TipoIva
        {
            get { return _tipoiva; }
            set
            {
                if (_tipoiva != value)
                {
                    _tipoiva = value;
                    RaisePropertyChanged("TipoIva");
                }
            }
        }

        private TipoConcepto _tipoconcepto;
        public TipoConcepto TipoConcepto
        {
            get { return _tipoconcepto; }
            set
            {
                if (_tipoconcepto != value)
                {
                    _tipoconcepto = value;
                    RaisePropertyChanged("TipoConcepto");
                }
            }
        }

       

        private TipoArticulos _tipoarticulo;
        public TipoArticulos TipoArticulo
        {
            get { return _tipoarticulo; }
            set
            {
                if (_tipoarticulo != value)
                {
                    _tipoarticulo = value;
                    RaisePropertyChanged("TipoArticulo");
                }
            }
        }


        private TipoFichero _tipofichero;
        public TipoFichero TipoFichero
        {
            get { return _tipofichero; }
            set
            {
                if (_tipofichero != value)
                {
                    _tipofichero = value;
                    RaisePropertyChanged("TipoFichero");
                }
            }
        }

        private TipoObra _tipoobra;
        public TipoObra TipoObra
        {
            get { return _tipoobra; }
            set
            {
                if (_tipoobra != value)
                {
                    _tipoobra = value;
                    RaisePropertyChanged("TipoObra");
                }
            }
        }

        private TipoOrigen _tipoorigen;
        public TipoOrigen TipoOrigen
        {
            get { return _tipoorigen; }
            set
            {
                if (_tipoorigen != value)
                {
                    _tipoorigen = value;
                    RaisePropertyChanged("TipoOrigen");
                }
            }
        }


        private TipoMantenimiento _tipomantenimiento;
        public TipoMantenimiento TipoMantenimiento
        {
            get { return _tipomantenimiento; }
            set
            {
                if (_tipomantenimiento != value)
                {
                    _tipomantenimiento = value;
                    RaisePropertyChanged("TipoMantenimiento");
                }
            }
        }

        private ContratosClientesBajaTemporal _contratoclientebajatemporal;
        public ContratosClientesBajaTemporal ContratosClientesBajaTemporal
        {

            get { return _contratoclientebajatemporal; }
            set
            {
                if (_contratoclientebajatemporal != value)
                {
                    _contratoclientebajatemporal = value;
                    RaisePropertyChanged("ContratosClientesBajaTemporal");
                }
            }
        }


        private ContratosProveedores _contratoproveedor;
        public ContratosProveedores ContratoProveedor
        {
            get { return _contratoproveedor; }
            set
            {
                if (_contratoproveedor != value)
                {
                    _contratoproveedor = value;
                    RaisePropertyChanged("ContratoProveedor");
                }
            }
        }

        private CatalogarConcepto _catalogarconcepto;
        public CatalogarConcepto CatalogarConcepto
        {
            get { return _catalogarconcepto; }
            set
            {
                if (_catalogarconcepto != value)
                {
                    _catalogarconcepto = value;
                    RaisePropertyChanged("CatalogarConcepto");
                }
            }
        }

        private DatosImprimirFactura _contratoclientedatoimprimir;
        public DatosImprimirFactura ContratosClientesDatoImprimir
        {

            get { return _contratoclientedatoimprimir; }
            set
            {
                if (_contratoclientedatoimprimir != value)
                {
                    _contratoclientedatoimprimir = value;
                    RaisePropertyChanged("ContratosClientesDatoImprimir");
                }
            }
        }

        private TipoConceptoArticulo _tipoconceptoarticulo;
        public TipoConceptoArticulo TipoConceptoArticulo
        {
            get { return _tipoconceptoarticulo; }
            set
            {
                if (_tipoconceptoarticulo != value)
                {
                    _tipoconceptoarticulo = value;
                    RaisePropertyChanged("TipoConceptoArticulo");
                }
            }
        }

        private Clasificacion _clasificacion;
        public Clasificacion Clasificacion
        {
            get { return _clasificacion; }
            set
            {
                if (_clasificacion != value)
                {
                    _clasificacion = value;
                    RaisePropertyChanged("Clasificacion");
                }
            }
        }

        private Inmuebles _inmueble;
        public Inmuebles Inmueble
        {
            get { return _inmueble; }
            set
            {
                if (_inmueble != value)
                {
                    _inmueble = value;
                    RaisePropertyChanged("Inmueble");
                }
            }
        }

        private TipoPlanta _planta;
        public TipoPlanta Planta
        {
            get { return _planta; }
            set
            {
                if (_planta != value)
                {
                    _planta = value;
                    RaisePropertyChanged("Planta");
                }
            }
        }

        private Periodicidad _periodicidadcoste;
        public Periodicidad PeriodicidadCoste
        {
            get { return _periodicidadcoste; }
            set
            {
                if (_periodicidadcoste != value)
                {
                    _periodicidadcoste = value;
                    RaisePropertyChanged("PeriodicidadCoste");
                }
            }
        }

        private Periodicidad _periodicidadrevision;
        public Periodicidad PeriodicidadRevision
        {
            get { return _periodicidadrevision; }
            set
            {
                if (_periodicidadrevision != value)
                {
                    _periodicidadrevision = value;
                    RaisePropertyChanged("PeriodicidadRevision");
                }
            }
        }


        private FormasDePago _formadepago;
        public FormasDePago FormaDePago
        {
            get { return _formadepago; }
            set
            {
                if (_formadepago != value)
                {
                    _formadepago = value;
                    RaisePropertyChanged("FormaDePago");
                }
            }
        }


        private Gastos _gasto;
        public Gastos Gasto
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

        private TipoGasto _tipogasto;
        public TipoGasto TipoGasto
        {
            get { return _tipogasto; }
            set
            {
                if (_tipogasto != value)
                {
                    _tipogasto = value;
                    RaisePropertyChanged("TipoGasto");
                }
            }
        }

        // Listas de entidades

        private List<Empresas> _empresas;
        public List<Empresas> Empresas
        {

            get { return _empresas; }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;
                    RaisePropertyChanged("Empresas");
                }
            }
        }

        private List<HistoricoEmpresas> _historicosempresas;
        public List<HistoricoEmpresas> HistoricoEmpresas
        {

            get { return _historicosempresas; }
            set
            {
                if (_historicosempresas != value)
                {
                    _historicosempresas = value;
                    RaisePropertyChanged("HistoricoEmpresas");
                }
            }
        }

        private List<HistoricoArticulos> _historicosarticulos;
        public List<HistoricoArticulos> HistoricoArticulos
        {

            get { return _historicosarticulos; }
            set
            {
                if (_historicosarticulos != value)
                {
                    _historicosarticulos = value;
                    RaisePropertyChanged("HistoricoArticulos");
                }
            }
        }

        private List<HistoricoContratosClientes> _historicoscontratosclientes;
        public List<HistoricoContratosClientes> HistoricoContratosClientes
        {

            get { return _historicoscontratosclientes; }
            set
            {
                if (_historicoscontratosclientes != value)
                {
                    _historicoscontratosclientes = value;
                    RaisePropertyChanged("HistoricoContratosClientes");
                }
            }
        }

        private List<HistoricoInmuebles> _historicoinmuebles;
        public List<HistoricoInmuebles> HistoricoInmuebles
        {

            get { return _historicoinmuebles; }
            set
            {
                if (_historicoinmuebles != value)
                {
                    _historicoinmuebles = value;
                    RaisePropertyChanged("HistoricoInmuebles");
                }
            }
        }

        private List<HistoricoContratosProveedores> _historicoscontratosproveedores;
        public List<HistoricoContratosProveedores> HistoricoContratosProveedores
        {

            get { return _historicoscontratosproveedores; }
            set
            {
                if (_historicoscontratosproveedores != value)
                {
                    _historicoscontratosproveedores = value;
                    RaisePropertyChanged("HistoricoContratosProveedores");
                }
            }
        }


        private List<Gastos> _gastos;
        public List<Gastos> Gastos
        {

            get { return _gastos; }
            set
            {
                if (_gastos != value)
                {
                    _gastos = value;
                    RaisePropertyChanged("Gastos");
                }
            }
        }

        private List<TipoGasto> _tiposgastos;
        public List<TipoGasto> TiposGasto
        {

            get { return _tiposgastos; }
            set
            {
                if (_tiposgastos != value)
                {
                    _tiposgastos = value;
                    RaisePropertyChanged("TiposGasto");
                }
            }
        }


        private List<TipoOrigen> _tipoorigenes;
        public List<TipoOrigen> TipoOrigenes
        {

            get { return _tipoorigenes; }
            set
            {
                if (_tipoorigenes != value)
                {
                    _tipoorigenes = value;
                    RaisePropertyChanged("TipoOrigenes");
                }
            }
        }

        private List<TipoInmuebles> _tipoinmuebles;
        public List<TipoInmuebles> TipoInmuebles
        {

            get { return _tipoinmuebles; }
            set
            {
                if (_tipoinmuebles != value)
                {
                    _tipoinmuebles = value;
                    RaisePropertyChanged("TipoInmuebles");
                }
            }
        }

        private List<Periodicidad> _periodicidades;
        public List<Periodicidad> Periodicidades
        {

            get { return _periodicidades; }
            set
            {
                if (_periodicidades != value)
                {
                    _periodicidades = value;
                    RaisePropertyChanged("Periodicidades");
                }
            }
        }


        private List<TipoPlanta> _plantas;
        public List<TipoPlanta> Plantas
        {

            get { return _plantas; }
            set
            {
                if (_plantas != value)
                {
                    _plantas = value;
                    RaisePropertyChanged("Plantas");
                }
            }
        }

        private List<Contactos> _contactos;
        public List<Contactos> Contactos
        {

            get { return _contactos; }
            set
            {
                if (_contactos != value)
                {
                    _contactos = value;
                    RaisePropertyChanged("Contactos");
                }
            }
        }

        private List<TipoArticulos> _tiposarticulos;
        public List<TipoArticulos> TipoArticulos
        {

            get { return _tiposarticulos; }
            set
            {
                if (_tiposarticulos != value)
                {
                    _tiposarticulos = value;
                    RaisePropertyChanged("TipoArticulos");
                }
            }
        }

        private List<VentaParcialInmueble> _ventaparcialinmuebles;
        public List<VentaParcialInmueble> VentaParcialInmuebles
        {
            get { return _ventaparcialinmuebles; }
            set
            {
                if (_ventaparcialinmuebles != value)
                {
                    _ventaparcialinmuebles = value;
                    RaisePropertyChanged("VentaParcialInmuebles");
                }
            }
        }


        private List<TipoFichero> _tipoficheros;
        public List<TipoFichero> TipoFicheros
        {

            get { return _tipoficheros; }
            set
            {
                if (_tipoficheros != value)
                {
                    _tipoficheros = value;
                    RaisePropertyChanged("TipoFicheros");
                }
            }
        }

        private List<TipoObra> _tipoobras;
        public List<TipoObra> TipoObras
        {

            get { return _tipoobras; }
            set
            {
                if (_tipoobras != value)
                {
                    _tipoobras = value;
                    RaisePropertyChanged("TipoObras");
                }
            }
        }

        private List<Usuarios> _usuarios;
        public List<Usuarios> Usuarios
        {

            get { return _usuarios; }
            set
            {
                if (_usuarios != value)
                {
                    _usuarios = value;
                    RaisePropertyChanged("Usuarios");
                }
            }
        }

        private List<Terceros> _terceros;
        public List<Terceros> Terceros
        {
            get { return _terceros; }
            set
            {
                _terceros = value;
                RaisePropertyChanged("Terceros");
            }
        }

        private List<Apuntes> _apuntes;
        public List<Apuntes> Apuntes
        {
            get { return _apuntes; }
            set
            {
                _apuntes = value;
                RaisePropertyChanged("Apuntes");
            }
        }


        private List<TipoMantenimiento> _tipomantenimientos;
        public List<TipoMantenimiento> TipoMantenimientos
        {

            get { return _tipomantenimientos; }
            set
            {
                if (_tipomantenimientos != value)
                {
                    _tipomantenimientos = value;
                    RaisePropertyChanged("TipoMantenimientos");
                }
            }
        }




        private List<TareaPeriodica> _tareasperiodicas;
        public List<TareaPeriodica> TareasPeriodicas
        {

            get { return _tareasperiodicas; }
            set
            {
                if (_tareasperiodicas != value)
                {
                    _tareasperiodicas = value;
                    RaisePropertyChanged("TareasPeriodicas");
                }
            }
        }

        private List<PeriodicidadServicio> _periodicidadesservicios;
        public List<PeriodicidadServicio> PeriodicidadesServicios
        {

            get { return _periodicidadesservicios; }
            set
            {
                if (_periodicidadesservicios != value)
                {
                    _periodicidadesservicios = value;
                    RaisePropertyChanged("PeriodicidadesServicios");
                }
            }
        }

        private List<HistorialOcupacion> _historialocupaciones;
        public List<HistorialOcupacion> HistorialOcupaciones
        {

            get { return _historialocupaciones; }
            set
            {
                if (_historialocupaciones != value)
                {
                    _historialocupaciones = value;
                    RaisePropertyChanged("HistorialOcupaciones");
                }
            }
        }

        private List<Periodicidad> _periodicidadcostes;
        public List<Periodicidad> PeriodicidadCostes
        {

            get { return _periodicidadcostes; }
            set
            {
                if (_periodicidadcostes != value)
                {
                    _periodicidadcostes = value;
                    RaisePropertyChanged("PeriodicidadCostes");
                }
            }
        }

        private List<Periodicidad> _periodicidadrevisiones;
        public List<Periodicidad> PeriodicidadRevisiones
        {

            get { return _periodicidadrevisiones; }
            set
            {
                if (_periodicidadrevisiones != value)
                {
                    _periodicidadrevisiones = value;
                    RaisePropertyChanged("PeriodicidadRevisiones");
                }
            }
        }

        private List<Provincias> _provincias;
        public List<Provincias> Provincias
        {

            get { return _provincias; }
            set
            {
                if (_provincias != value)
                {
                    _provincias = value;
                    RaisePropertyChanged("Provincias");
                }
            }
        }

        private List<TipoIpc> _tipoIpcs;
        public List<TipoIpc> TipoIpcs
        {

            get { return _tipoIpcs; }
            set
            {
                if (_tipoIpcs != value)
                {
                    _tipoIpcs = value;
                    RaisePropertyChanged("TipoIpcs");
                }
            }
        }


        private List<Licencias> _licencias;
        public List<Licencias> Licencias
        {

            get { return _licencias; }
            set
            {
                if (_licencias != value)
                {
                    _licencias = value;
                    RaisePropertyChanged("Licencias");
                }
            }
        }



        private TipoLicencia _tipolicencia;
        public TipoLicencia TipoLicencia
        { 

            get { return _tipolicencia; }
            set
            {
                if (_tipolicencia != value)
                {
                    _tipolicencia = value;
                    RaisePropertyChanged("TipoLicencia");
                }
            }
        }

        private List<TipoLicencia> _tiposlicencia;
        public List<TipoLicencia> TiposLicencia
        {
            get { return _tiposlicencia; }
            set
            {
                if (_tiposlicencia != value)
                {
                    _tiposlicencia = value;
                    RaisePropertyChanged("TiposLicencia");
                }
            }
        }


        private List<ConceptoFacturacion> _conceptosfacturacion;
        public List<ConceptoFacturacion> ConceptosFacturacion
        {

            get { return _conceptosfacturacion; }
            set
            {
                if (_conceptosfacturacion != value)
                {
                    _conceptosfacturacion = value;
                    RaisePropertyChanged("ConceptosFacturacion");
                }
            }
        }


        private List<ReferenciasCatastrales> _referenciascatastrales;
        public List<ReferenciasCatastrales> ReferenciasCatastrales
        {

            get { return _referenciascatastrales; }
            set
            {
                if (_referenciascatastrales != value)
                {
                    _referenciascatastrales = value;
                    RaisePropertyChanged("ReferenciasCatastrales");
                }
            }
        }


        private List<CatalogarConcepto> _catalogarconceptos;
        public List<CatalogarConcepto> CatalogarConceptos
        {

            get { return _catalogarconceptos; }
            set
            {
                if (_catalogarconceptos != value)
                {
                    _catalogarconceptos = value;
                    RaisePropertyChanged("CatalogarConceptos");
                }
            }
        }
        
        private List<TipoIva> _tipoivas;
        public List<TipoIva> TipoIvas
        {

            get { return _tipoivas; }
            set
            {
                if (_tipoivas != value)
                {
                    _tipoivas = value;
                    RaisePropertyChanged("TipoIvas");
                }
            }
        }

        private List<Swift> _swifts;
        public List<Swift> Swifts
        {

            get { return _swifts; }
            set
            {
                if (_swifts != value)
                {
                    _swifts = value;
                    RaisePropertyChanged("Swifts");
                }
            }
        }

        private List<Normas> _normas;
        public List<Normas> Normas
        {

            get { return _normas; }
            set
            {
                if (_normas != value)
                {
                    _normas = value;
                    RaisePropertyChanged("Normas");
                }
            }
        }

        private List<TipoInstalacion> _tipointalaciones;
        public List<TipoInstalacion> TipoInstalaciones
        {

            get { return _tipointalaciones; }
            set
            {
                if (_tipointalaciones != value)
                {
                    _tipointalaciones = value;
                    RaisePropertyChanged("TipoInstalaciones");
                }
            }
        }

        private List<TipoMedida> _tipomedidas;
        public List<TipoMedida> TipoMedidas
        {

            get { return _tipomedidas; }
            set
            {
                if (_tipomedidas != value)
                {
                    _tipomedidas = value;
                    RaisePropertyChanged("TipoMedidas");
                }
            }
        }

        private List<TipoConcepto> _tipoconceptos;
        public List<TipoConcepto> TipoConceptos
        {

            get { return _tipoconceptos; }
            set
            {
                if (_tipoconceptos != value)
                {
                    _tipoconceptos = value;
                    RaisePropertyChanged("TipoConceptos");
                }
            }
        }

        private List<TipoConceptoArticulo> _tipoconceptoarticulos;
        public List<TipoConceptoArticulo> TipoConceptoArticulos
        {

            get { return _tipoconceptoarticulos; }
            set
            {
                if (_tipoconceptoarticulos != value)
                {
                    _tipoconceptoarticulos = value;
                    RaisePropertyChanged("TipoConceptoArticulos");
                }
            }
        }

        private List<ContratosClientesBajaTemporal> _contratosclientesbajastemporal;
        public List<ContratosClientesBajaTemporal> ContratosClientesBajasTemporal
        {

            get { return _contratosclientesbajastemporal; }
            set
            {
                if (_contratosclientesbajastemporal != value)
                {
                    _contratosclientesbajastemporal = value;
                    RaisePropertyChanged("ContratosClientesBajasTemporal");
                }
            }
        }

        private List<ContratoClienteConceptoFactura> _contratosclientesconceptosfactura;
        public List<ContratoClienteConceptoFactura> ContratosClientesConceptosFactura
        {

            get { return _contratosclientesconceptosfactura; }
            set
            {
                if (_contratosclientesconceptosfactura != value)
                {
                    _contratosclientesconceptosfactura = value;
                    RaisePropertyChanged("ContratosClientesConceptosFactura");
                }
            }
        }


        private List<DatosImprimirFactura> _contratosclientesdatosimprimir;
        public List<DatosImprimirFactura> ContratosClienteDatosImprimir
        {

            get { return _contratosclientesdatosimprimir; }
            set
            {
                if (_contratosclientesdatosimprimir != value)
                {
                    _contratosclientesdatosimprimir = value;
                    RaisePropertyChanged("ContratosClienteDatosImprimir");
                }
            }
        }

        private List<Clasificacion> _clasificaciones;
        public List<Clasificacion> Clasificaciones
        {

            get { return _clasificaciones; }
            set
            {
                if (_clasificaciones != value)
                {
                    _clasificaciones = value;
                    RaisePropertyChanged("Clasificaciones");
                }
            }
        }

        private List<ContratosClientes> _contratosclientes;
        public List<ContratosClientes> ContratosClientes
        {

            get { return _contratosclientes; }
            set
            {
                if (_contratosclientes != value)
                {
                    _contratosclientes = value;
                    RaisePropertyChanged("ContratosClientes");
                }
            }
        }

        private List<ModalidadFactura> _modalidadfacturas;
        public List<ModalidadFactura> ModalidadFacturas
        {

            get { return _modalidadfacturas; }
            set
            {
                if (_modalidadfacturas != value)
                {
                    _modalidadfacturas = value;
                    RaisePropertyChanged("ModalidadFacturas");
                }
            }
        }

        private List<ContratosProveedores> _contratosproveedores;
        public List<ContratosProveedores> ContratosProveedores
        {

            get { return _contratosproveedores; }
            set
            {
                if (_contratosproveedores != value)
                {
                    _contratosproveedores = value;
                    RaisePropertyChanged("ContratosProveedores");
                }
            }
        }

        private List<Inmuebles> _inmuebles;
        public List<Inmuebles> Inmuebles
        {

            get { return _inmuebles; }
            set
            {
                if (_inmuebles != value)
                {
                    _inmuebles = value;
                    RaisePropertyChanged("Inmuebles");
                }
            }
        }

        private List<Facturacion> _facturas;
        public List<Facturacion> Facturas
        {

            get { return _facturas; }
            set
            {
                if (_facturas != value)
                {
                    _facturas = value;
                    RaisePropertyChanged("Facturas");
                }
            }
        }

        private List<HistorialObra> _obras;
        public List<HistorialObra> Obras
        {

            get { return _obras; }
            set
            {
                if (_obras != value)
                {
                    _obras = value;
                    RaisePropertyChanged("Obras");
                }
            }
        }

        private List<ObrasFichero> _obrasfichero;
        public List<ObrasFichero> ObrasFichero
        {

            get { return _obrasfichero; }
            set
            {
                if (_obrasfichero != value)
                {
                    _obrasfichero = value;
                    RaisePropertyChanged("ObrasFichero");
                }
            }
        }

        private List<HistoricoSeguros> _historicosseguros;
        public List<HistoricoSeguros> HistoricosSeguros
        {

            get { return _historicosseguros; }
            set
            {
                if (_historicosseguros != value)
                {
                    _historicosseguros = value;
                    RaisePropertyChanged("HistoricosSeguros");
                }
            }
        }

        private List<InmuebleCentroCoste> _inmueblescentroscostes;
        public List<InmuebleCentroCoste> InmueblesCentrosCostes
        {

            get { return _inmueblescentroscostes; }
            set
            {
                if (_inmueblescentroscostes != value)
                {
                    _inmueblescentroscostes = value;
                    RaisePropertyChanged("InmueblesCentrosCostes");
                }
            }
        }


        private List<HistoricoTasaciones> _historicostasaciones;
        public List<HistoricoTasaciones> HistoricosTasaciones
        {

            get { return _historicostasaciones; }
            set
            {
                if (_historicostasaciones != value)
                {
                    _historicostasaciones = value;
                    RaisePropertyChanged("HistoricosTasaciones");
                }
            }
        }

        private List<Mantenimientos> _mantenimientos;
        public List<Mantenimientos> Mantenimientos
        {

            get { return _mantenimientos; }
            set
            {
                if (_mantenimientos != value)
                {
                    _mantenimientos = value;
                    RaisePropertyChanged("Mantenimientos");
                }
            }
        }



        private List<Incidencias> _incidencias;
        public List<Incidencias> Incidencias
        {

            get { return _incidencias; }
            set
            {
                if (_incidencias != value)
                {
                    _incidencias = value;
                    RaisePropertyChanged("Incidencias");
                }
            }
        }

        private List<Tareas> _tareas;
        public List<Tareas> Tareas
        {

            get { return _tareas; }
            set
            {
                if (_tareas != value)
                {
                    _tareas = value;
                    RaisePropertyChanged("Tareas");
                }
            }
        }

        private List<Articulos> _articulos;
        public List<Articulos> Articulos
        {

            get { return _articulos; }
            set
            {
                if (_articulos != value)
                {
                    _articulos = value;
                    RaisePropertyChanged("Articulos");
                }
            }
        }


        private List<FormasDePago> _formasdepago;
        public List<FormasDePago> FormasDePago
        {

            get { return _formasdepago; }
            set
            {
                if (_formasdepago != value)
                {
                    _formasdepago = value;
                    RaisePropertyChanged("FormasDePago");
                }
            }
        }


        private string _textdeleteitem;
        public string TextDeleteItem
        {
            get { return _textdeleteitem; }
            set
            {
                if (_textdeleteitem != value)
                {
                    _textdeleteitem = value;
                    RaisePropertyChanged("TextDeleteItem");
                }
            }
        }

        public Visibility MostrarMensajes
        {
            get
            {
                if (string.IsNullOrWhiteSpace(mensaje))
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        private ICommand cerrarMensajes;
        public ICommand CerrarMensajes
        {
            get
            {
                if (cerrarMensajes == null)
                {
                    cerrarMensajes = new RelayCommand(p => { Mensaje = ""; });
                }

                return cerrarMensajes;
            }
        }

        private ICommand _changePageCommand;
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        private ICommand _openWindowCommand;
        public ICommand OpenWindowCommand
        {
            get
            {
                if (_openWindowCommand == null)
                {
                    _openWindowCommand = new RelayCommand(
                        p => OpenNewWindow((IWindow)p),
                        p => p is IWindow);
                }

                return _openWindowCommand;
            }
        }

        private IPageViewModel _currentPageViewModel;
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        private IWindow _currentWindowOpen;
        public IWindow CurrentWindowOpen
        {
            get
            {
                return _currentWindowOpen;
            }
            set
            {
                if (_currentWindowOpen != value)
                {
                    _currentWindowOpen = value;
                    OnPropertyChanged("CurrentWindowOpen");
                }
            }
        }
        public void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        public void OpenNewWindow(IWindow windowToOpen)
        {
            if (!AppWindows.Contains(windowToOpen))
                AppWindows.Add(windowToOpen);

            CurrentWindowOpen = AppWindows
                .FirstOrDefault(vm => vm == windowToOpen);

            Type type = Type.GetType("CFAInmuebles.WPF." + windowToOpen.WindowName);
            Type typeVM = Type.GetType("CFAInmuebles.WPF." + windowToOpen.WindowName + "VM");

            if (type != null)
            {
                if (!IsWindowOpen<Window>("CFAInmuebles.WPF." + windowToOpen.WindowName))
                {
                    var ventana = Activator.CreateInstance(type);
                    ((Window)ventana).DataContext = Activator.CreateInstance(typeVM);
                    ((Window)ventana).Show();                     
                }  
            }
        }


        public List<IPageViewModel> _pageViewModels;
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public List<IWindow> _appWindows;
        public List<IWindow> AppWindows
        {
            get
            {
                if (_appWindows == null)
                    _appWindows = new List<IWindow>();

                return _appWindows;
            }
        }

        public ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData());
                }
                return _modifyCommand;
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(p => LoadData());
                }
                return _loadCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(p => DeleteData());
                }
                return _deleteCommand;
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (_volverCommand == null)
                {
                    _volverCommand = new RelayCommand(p => SearchData());
                }
                return _volverCommand;
            }
        }

        public ICommand VolverCommand
        {
            get
            {
                if (_volverCommand == null)
                {
                    _volverCommand = new RelayCommand(p => VolverListado());
                }
                return _volverCommand;
            }
        }

        public ICommand ImportCommand
        {
            get
            {
                if (_importCommand == null)
                {
                    _importCommand = new RelayCommand(p => ImportData());
                }
                return _importCommand;
            }
        }

        public ICommand GenerateCommand
        {
            get
            {
                if (_generateCommand == null)
                {
                    _generateCommand = new RelayCommand(p => GenerateFile());
                }
                return _generateCommand;
            }
        }

        protected virtual void LoadData()  { }
        protected virtual void ModifyData() { }
        protected virtual void DeleteData() { }
        protected virtual void SearchData() { }
        protected virtual void VolverListado() { }
        protected virtual void ImportData() { }
        protected virtual void GenerateFile() { }

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            var window = Application.Current.Windows.OfType<T>().Where(w => w.ToString().Equals(name)).FirstOrDefault();

            if (window != null)
            {
                window.Activate();
                window.WindowState = WindowState.Normal;
                window.Topmost = true;
                return true;
            }

            return false;          
        }

        public Dictionary<string, string> Errors = new Dictionary<string, string>();
        
        

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return ValidateProperty(columnName);
            }
        }

        protected string ValidateProperty(string propertyName)
        {
            return Errors.ContainsKey(propertyName) ? Errors[propertyName] : null;
        }

        protected void Trazabilidad(string bloque, string mantenimiento, string entidad, string accion, string descripcion)
        { 

            log = new Logs();
            log.Usuario = userName;
            log.Bloque = bloque;
            log.Mantenimiento = mantenimiento;
            log.Entidad = entidad;
            log.Fecha = DateTime.Now;
            log.Accion = accion;
            log.Descripcion = descripcion;
            db.Logs.Add(log);
            db.SaveChanges();
        }

         protected void SetError(string propertyName, string errorMessage)
        {
            if (String.IsNullOrEmpty(propertyName))
                return;

            if (!String.IsNullOrEmpty(errorMessage))
            {
                if (Errors.ContainsKey(propertyName))
                    Errors[propertyName] = errorMessage;
                else
                    Errors.Add(propertyName, errorMessage);
            }

            else if (Errors.ContainsKey(propertyName))
                Errors.Remove(propertyName);

            RaisePropertyChanged("Errors");
            RaisePropertyChanged("Error");
            RaisePropertyChanged("Item[]");
        }
    }

    public class IntToStringConverter : IValueConverter
        {
            public decimal? EmptyStringValue { get; set; }

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value == null)
                    return null;
                else if (value is string)
                    return value;
                else if ((value is decimal && (decimal)value == EmptyStringValue) || (value is int && (int)value == EmptyStringValue))
                    return string.Empty;
                else
                    return value.ToString();
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is string)
                {
                    string s = (string)value;

                    s =  s.Replace('.', ',');

                    if (double.TryParse(s, out double numValue))
                        return numValue;
                    else
                        return null;
                }

                return value;
            }
        }

    
    public class StringToIntValidationRule : ValidationRule
        {
            public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
            {
                decimal i;
                if (decimal.TryParse(value.ToString(), out i) || String.IsNullOrEmpty(value.ToString()))
                {
                    if (String.IsNullOrEmpty(value.ToString()))
                    {
                        value = 0;
                    }
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "Este campo debe ser numérico.");
            }
        }

        
}


