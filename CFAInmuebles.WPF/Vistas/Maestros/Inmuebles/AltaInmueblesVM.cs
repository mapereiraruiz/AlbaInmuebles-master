using CFAInmuebles.Domain.Models;
using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using CFAInmuebles.Infrastructure.Data;
using System.Reflection;
using System.Collections.Generic;

namespace CFAInmuebles.WPF
{
    public class AltaInmueblesVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Inmuebles entity;
        public Inmuebles newentity;

        private HomeInmueblesVM baseVM;
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

        public AltaInmueblesVM(HomeInmueblesVM baseVM, Inmuebles entidad = null)
        {
            entity = new Inmuebles();

            if (entidad != null)
            {
                var properties = typeof(Inmuebles).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                  .Where(p => !(p.PropertyType.IsInterface && p.PropertyType.IsGenericType &&
                                p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                               && p.CanWrite).ToList();
                properties.ForEach(p =>
                {
                    p.GetValue(entidad);
                    p.SetValue(entity, p.GetValue(entidad));
                });
            }
            this.baseVM = baseVM; 
        }

        public string Name
        {
            get
            {
                return "Alta Inmuebles";
            }
        }
        public int MaxInmueble
        {
            get { return 100; }
        }
        public int MaxCalle
        {
            get { return 100; }
        }
        public int MaxMunicipio
        {
            get { return 100; }
        }
        public int MaxCodigoPostal
        {
            get { return 5; }
        }
        public int MaxReferenciaCatastralPrincipal
        {
            get { return 150; }
        }
        public Visibility MostrarBotones
        {
            get
            {
                if (entity != null && entity.IdInmueble == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        public new string Inmueble
        {
            get {
                CheckValidationState("Inmueble", _inmueble); 
                return _inmueble; }
            set
            {
                if (_inmueble != value)
                {
                    CheckValidationState("Inmueble", _inmueble);                    
                    _inmueble = value;
                    entity.Inmueble = _inmueble;
                    RaisePropertyChanged("Inmueble");
                }
            }
        }

        public new Empresas Empresa
        {
            get
            {
                CheckValidationState("Empresa", _empresa);
                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    CheckValidationState("Empresa", _empresa);
                    _empresa = value;
                    entity.IdEmpresaNavigation = _empresa;
                    RaisePropertyChanged("Empresa");
                }
            }
        }
        public new TipoInmuebles TipoInmueble
        {
            get
            {
                CheckValidationState("TipoInmueble", _tipoinmueble);
                return _tipoinmueble;
            }
            set
            {
                if (_tipoinmueble != value)
                {
                    CheckValidationState("TipoInmueble", _tipoinmueble);
                    _tipoinmueble = value;
                    entity.IdTipoInmuebleNavigation = _tipoinmueble;
                    RaisePropertyChanged("TipoInmueble");
                }
            }
        }

        public new Provincias Provincia
        {
            get
            {
                CheckValidationState("Provincia", _provincia);
                return _provincia;
            }
            set
            {
                if (_provincia != value)
                {
                    CheckValidationState("Provincia", _provincia);
                    _provincia = value;
                    entity.IdProvinciaNavigation = _provincia;
                    RaisePropertyChanged("Provincia");
                }
            }
        }


        public string Calle
        {
            get {
                CheckValidationState("Calle", _calle); 
                return _calle; }
            set
            {
                if (_calle != value)
                {
                    CheckValidationState("Calle", _calle);
                    _calle = value;
                    entity.Calle = _calle;
                    RaisePropertyChanged("Calle");
                }
            }
        }

        public string Municipio
        {
            get {
                CheckValidationState("Municipio", _municipio); 
                return _municipio; }
            set
            {
                if (_municipio != value)
                {
                    CheckValidationState("Municipio", _municipio);
                    _municipio = value;
                    entity.Municipio = _municipio;
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
                    entity.CodigoPostal = _codigopostal;
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
                     entity.ReferenciaCatastralGeneral = _referenciacastastralprincipal;
                    RaisePropertyChanged("ReferenciaCatastralPrincipal");
                }
            }
        }

        public string SuperficieParcela
        {
            get {
                CheckValidationState("SuperficieParcela", _superficieparcela);
                return _superficieparcela; }
            set
            {
                if (_superficieparcela != value)
                {
                    CheckValidationState("SuperficieParcela", _superficieparcela);
                    _superficieparcela = value;

                    if (double.TryParse(SuperficieParcela, out double numValue))
                        entity.SuperficieParcela = double.Parse(SuperficieParcela);
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
                    entity.UsoPropio = _usopropio;
                    RaisePropertyChanged("UsoPropio");
                }
            }
        }

        public string SuperficieRegistralS
        {
            get {
                CheckValidationState("SuperficieRegistralS", _superficieregistrals);
                return _superficieregistrals; }
            set
            {
                if (_superficieregistrals != value)
                {
                    CheckValidationState("SuperficieRegistralS", _superficieregistrals);
                    _superficieregistrals = value;

                    if (double.TryParse(SuperficieRegistralS, out double numValue))
                        entity.SuperficieRegistralS = double.Parse(SuperficieRegistralS);

                    RaisePropertyChanged("SuperficieRegistralS");
                }
            }
        }

        public string SuperficieRegistralB
        {
            get {
                CheckValidationState("SuperficieRegistralB", _superficieregistralb); 
                return _superficieregistralb; }
            set
            {
                if (_superficieregistralb != value)
                {
                    CheckValidationState("SuperficieRegistralB", _superficieregistralb);
                    _superficieregistralb = value;

                    if (double.TryParse(SuperficieRegistralB, out double numValue))
                        entity.SuperficieRegistralB = double.Parse(SuperficieRegistralB);

                    RaisePropertyChanged("SuperficieRegistralB");
                }
            }
        }

        public string SuperficieAlbaS
        {
            get {
                CheckValidationState("SuperficieAlbaS", _superficiealbas); 
                return _superficiealbas; }
            set
            {
                if (_superficiealbas != value)
                {
                    CheckValidationState("SuperficieAlbaS", _superficiealbas);
                    _superficiealbas = value;

                    if (double.TryParse(SuperficieAlbaS, out double numValue))
                        entity.SuperficieAlbaS = double.Parse(SuperficieAlbaS);

                    RaisePropertyChanged("SuperficieAlbaS");
                }
            }
        }


        public string SuperficieAlbaB
        {
            get {
                CheckValidationState("SuperficieAlbaB", _superficiealbab); 
                return _superficiealbab; }
            set
            {
                if (_superficiealbab != value)
                {
                    CheckValidationState("SuperficieAlbaB", _superficiealbab);
                    _superficiealbab = value;

                    if (double.TryParse(SuperficieAlbaB, out double numValue))
                        entity.SuperficieAlbaB = double.Parse(SuperficieAlbaB);

                    RaisePropertyChanged("SuperficieAlbaB");
                }
            }
        }


        public string SuperficieCatastralS
        {
            get {
                CheckValidationState("SuperficieCatastralS", _superficiecatastrals); 
                return _superficiecatastrals; }
            set
            {
                if (_superficiecatastrals != value)
                {
                    CheckValidationState("SuperficieCatastralS", _superficiecatastrals);
                    _superficiecatastrals = value;

                    if (double.TryParse(SuperficieCatastralS, out double numValue))
                        entity.SuperficieCatastralS = double.Parse(SuperficieCatastralS);

                    RaisePropertyChanged("SuperficieCatastralS");
                }
            }
        }


        public string SuperficieCatastralB
        {
            get {
                CheckValidationState("SuperficieCatastralB", _superficiecatastralb); 
                return _superficiecatastralb; }
            set
            {
                if (_superficiecatastralb != value)
                {
                    CheckValidationState("SuperficieCatastralB", _superficiecatastralb);
                    _superficiecatastralb = value;

                    if (double.TryParse(SuperficieCatastralB, out double numValue))
                        entity.SuperficieCatastralB = double.Parse(SuperficieCatastralB);

                    RaisePropertyChanged("SuperficieCatastralB");
                }
            }
        }

        public string NumPlazaRegistral
        {
            get {
                CheckValidationState("NumPlazaRegistral", _numplazaregistral);
                return _numplazaregistral; }
            set
            {
                if (_numplazaregistral != value)
                {
                    CheckValidationState("NumPlazaRegistral", _numplazaregistral);
                    _numplazaregistral = value;

                    if (int.TryParse(NumPlazaRegistral, out int numValue))
                        entity.NumPlazaRegistral = int.Parse(NumPlazaRegistral);

                    RaisePropertyChanged("NumPlazaRegistral");
                }
            }
        }


        public string NumPlazaCatastral
        {
            get {
                CheckValidationState("NumPlazaCatastral", _numplazacatastral); 
                return _numplazacatastral; }
            set
            {
                if (_numplazacatastral != value)
                {
                    CheckValidationState("NumPlazaCatastral", _numplazacatastral);
                    _numplazacatastral = value;

                    if (int.TryParse(NumPlazaCatastral, out int numValue))
                        entity.NumPlazaCatastral = int.Parse(NumPlazaCatastral);

                    RaisePropertyChanged("NumPlazaCatastral");
                }
            }
        }


        public string NumPlazaAlba
        {
            get {
                CheckValidationState("NumPlazaAlba", _numplazaalba); 
                return _numplazaalba; }
            set
            {
                if (_numplazaalba != value)
                {
                    CheckValidationState("NumPlazaAlba", _numplazaalba);
                    _numplazaalba = value;

                    if (int.TryParse(NumPlazaAlba, out int numValue))
                        entity.NumPlazaAlba = int.Parse(NumPlazaAlba);

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
                Empresa = entity.IdEmpresaNavigation;
                TipoInmueble = entity.IdTipoInmuebleNavigation;
                Provincia = entity.IdProvinciaNavigation;
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

                if (entity.FechaCompra.ToShortDateString() == "01/01/0001")
                    entity.FechaCompra = DateTime.Now;

                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Ficha Inmuebles");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                entity.FechaSistema = DateTime.Now;
                entity.IdUsuarioNavigation = UserId;

                var model = db.Inmuebles.Find(entity?.IdInmueble);

                if (model == null)
                {
                    db.Inmuebles.Add(entity);
                    accion = "Alta";
                }
                else
                {
                    var newEntity = db.Inmuebles.ApplyValues(entity, entity.IdInmueble);
                }

                db.SaveChanges();

                //Grabamos cambios
                if (accion == "Update")
                {
                    var historico = new HistoricoInmuebles();
                    historico.Abreviatura = entity.Abreviatura;
                    historico.ArchivoDigital = entity.ArchivoDigital;
                    historico.Calle = entity.Calle;
                    historico.CodigoPostal = entity.CodigoPostal;
                    historico.FechaAeat = entity.FechaAeat;
                    historico.FechaCompra = entity.FechaCompra;
                    historico.FechaEliminacion = entity.FechaEliminacion;
                    historico.FechaSistema = DateTime.Now;
                    historico.FechaVenta = entity.FechaVenta;
                    historico.HistorialIncidencia = entity.HistorialIncidencia;
                    historico.IdConceptoFacturacionNavigation = entity.IdConceptoFacturacionNavigation;
                    historico.IdEmpresaNavigation = entity.IdEmpresaNavigation;
                    historico.IdInmuebleNavigation = model;
                    historico.IdInmuebleCentroCosteNavigation = entity.IdInmuebleCentroCosteNavigation;
                    historico.IdLicencia = entity.IdLicencia;
                    historico.IdProvinciaNavigation = entity.IdProvinciaNavigation;
                    historico.IdTipoInmuebleNavigation = entity.IdTipoInmuebleNavigation;
                    historico.IdTipoArticuloNavigation = entity.IdTipoArticuloNavigation;
                    historico.IdUsuarioNavigation = UserId;
                    historico.ImporteCompra = entity.ImporteCompra;
                    historico.ImporteVenta = entity.ImporteVenta;
                    historico.Inmueble = entity.Inmueble;
                    historico.Municipio = entity.Municipio;
                    historico.NumPlazaAlba = entity.NumPlazaAlba;
                    historico.NumPlazaCatastral = entity.NumPlazaCatastral;
                    historico.NumPlazaRegistral = entity.NumPlazaRegistral;
                    historico.ReferenciaCatastralGeneral = entity.ReferenciaCatastralGeneral;
                    historico.SuperficieAlbaB = entity.SuperficieAlbaB;
                    historico.SuperficieAlbaS = entity.SuperficieAlbaS;
                    historico.SuperficieCatastralB = entity.SuperficieCatastralB;
                    historico.SuperficieCatastralS = entity.SuperficieCatastralS;
                    historico.SuperficieParcela = entity.SuperficieParcela;
                    historico.SuperficieRegistralB = entity.SuperficieRegistralB;
                    historico.SuperficieRegistralS = entity.SuperficieRegistralS;
                    historico.TasaBasuras = entity.TasaBasuras;
                    historico.TasaCapitalizacion = entity.TasaCapitalizacion;
                    historico.UsoPropio = entity.UsoPropio;
                    historico.ValorEdificio = entity.ValorEdificio;
                    historico.ValorEdificioAeat = entity.ValorEdificioAeat;
                    historico.ValorPlazaGaraje = entity.ValorPlazaGaraje;
                    historico.ValorSuelo = entity.ValorSuelo;
                    historico.ValorSueloAeat = entity.ValorSueloAeat;
                    historico.ServiciosConserjeria = entity.ServiciosConserjeria;
                    historico.ServiciosVigilancia24h = entity.ServiciosVigilancia24h;
                    historico.ServiciosPersonalMantenimiento = entity.ServiciosPersonalMantenimiento;
                    historico.ServiciosRecepcion = entity.ServiciosRecepcion;
                    historico.ServiciosPaqueteria = entity.ServiciosPaqueteria;
                    historico.InstalacionesAireAcondicionado = entity.InstalacionesAireAcondicionado;
                    historico.InstalacionesCalefaccion = entity.InstalacionesCalefaccion;
                    historico.InstalacionesAscensores = entity.InstalacionesAscensores;
                    historico.InstalacionesIncendios = entity.InstalacionesIncendios;
                    historico.InstalacionesControlAccesos = entity.InstalacionesControlAccesos;
                    historico.InstalacionesCCTV = entity.InstalacionesCCTV;
                    historico.InstalacionesIluminacion = entity.InstalacionesIluminacion;
                    historico.InstalacionesPCI = entity.InstalacionesPCI;
                    historico.InstalacionesScanner = entity.InstalacionesScanner;
                    historico.GeneralDescripcion = entity.GeneralDescripcion;
                    historico.GeneralUso = entity.GeneralUso;
                    historico.GeneralSuperficie = entity.GeneralSuperficie;
                    historico.GeneralAparcamiento = entity.GeneralAparcamiento;
                    historico.GeneralNumeroPlazas = entity.GeneralNumeroPlazas;
                    historico.CalidadesFalsosTechos = entity.CalidadesFalsosTechos;
                    historico.CalidadesSuelosTecnicos = entity.CalidadesSuelosTecnicos;
                    historico.CalidadesPavimentos = entity.CalidadesPavimentos;
                    historico.CalidadesFachada = entity.CalidadesFachada;
                    historico.CalidadesCarpinterias = entity.CalidadesCarpinterias;
                    historico.CalidadesRevestimientos = entity.CalidadesRevestimientos;
                    historico.CalidadesAseosAdaptados = entity.CalidadesAseosAdaptados;
                    db.HistoricoInmuebles.Add(historico);
                    db.SaveChanges();
                }

                //Le damos permiso al usuario sobre ese inmueble

                if (accion == "Alta")
                {
                //    var model = db.Inmuebles.Find(entity.IdInmueble);
                    var usuarioInmueble = new UsuarioInmueble();
                    usuarioInmueble.IdInmueble = entity.IdInmueble;
                    usuarioInmueble.IdUsuarioNavigation = UserId;
                    db.UsuarioInmueble.Add(usuarioInmueble);
                    db.SaveChanges();
                }
                
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, accion, "Ficha Inmuebles");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Inmuebles").FirstOrDefault());
            }

            else
                Mensaje = baseVM.Mensaje; 
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.Inmuebles.Find(entity.IdInmueble);

            //Comprobamos que no tenga ninguna entidad relacionada

            var ficheroobras = db.ObrasFichero.Where(m => m.IdFichero == entity.IdInmueble && m.IdTipoFicheroNavigation.Valor == "Inmueble").Select(m => m.IdHistorialObra).ToList();
            var historialobra = db.HistorialObra.Where(m => m.FechaEliminacion == null && ficheroobras.Contains(m.IdHistorialObra)).Any();
            var articulos = db.Articulos.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Any();
            var contratosclientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Any();      
            var contratosproveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Any();
            var historicofacturacion = db.HistoricoFacturacion.Where(m => m.IdInmueble == entity.IdInmueble).Any();
            var historicoseguros = db.HistoricoSeguros.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Any();
            var historicotasaciones = db.HistoricoTasaciones.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Any();
            var referenciascatastrales = db.ReferenciasCatastrales.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Any();
            var superficietasacion = db.SuperficieTasacion.Where(m => m.IdInmueble == entity.IdInmueble).Any();
      
            if (!historialobra && articulos && !contratosclientes && !contratosproveedores && !historicofacturacion && !historicoseguros && !historicotasaciones && !referenciascatastrales && !superficietasacion)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Inmmuebles").FirstOrDefault();
                viewmodel = new DeleteInmueblesVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = String.Empty;

                if (historialobra)
                    Mensaje = "Desvincule los Historiales de Obra vinculados a este Inmueble para poder eliminarlo. ";
                if (articulos)
                    Mensaje += "Desvincule los Artículos vinculados a este Inmueble para poder eliminarlo. ";
                if (contratosclientes)
                    Mensaje += "Desvincule los Contratos Clientes vinculados a este Inmueble para poder eliminarlo. ";
                if (contratosproveedores)
                    Mensaje += "Desvincule los Contratos Proveedores vinculados a este Inmueble para poder eliminarlo. ";
                if (historicofacturacion)
                    Mensaje += "Desvincule los Históricos de Facturación vinculados a este Inmueble para poder eliminarlo. ";
                if (historicoseguros)
                    Mensaje += "Desvincule los Históricos de seguros vinculados a este Inmueble para poder eliminarlo. ";
                if (historicotasaciones)
                    Mensaje += "Desvincule los Históricos de Tasaciones a este Inmueble para poder eliminarlo. ";
                if (referenciascatastrales)
                    Mensaje += "Desvincule las Referencias Catastrales vinculados a este Inmueble para poder eliminarlo. ";
                if (superficietasacion)
                    Mensaje += "Desvincule las Superficies de Tasación vinculadas a este Inmueble para poder eliminarlo. ";
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Inmuebles").FirstOrDefault());
        }
        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            baseVM.entity = entity;
            baseVM.CheckValidationState(propertyName, proposedValue);

            if (propertyName == "Inmueble")
            {
                var existe = db.Inmuebles.Where(m => m.Inmueble == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace(" * El campo Inmueble es obligatorio. ", "");
                    Mensaje = Mensaje.Replace(" * Ya existe un Inmueble con ese valor. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Inmueble es obligatorio. ";
                    return false;
                }

                else if (existe != null && existe.IdInmueble != entity.IdInmueble)
                {
                    SetError(propertyName, "*");
                    Mensaje += " * Ya existe un Inmueble con ese valor. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Calle")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace(" * El campo Calle es obligatorio. ", "");
                  
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Calle es obligatorio. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Municipio")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace(" * El campo Municipio es obligatorio. ", "");

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Municipio es obligatorio. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Empresa")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace(" * El campo Empresa es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Empresa es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "TipoInmueble")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace(" * El campo Tipo Inmueble es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Tipo Inmueble es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Provincia")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace(" * El campo Provincia es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Provincia es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "NumUnidad")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo NumUnidad debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo NumUnidad debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "SuperficieParcela")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Superficie Parcela debe ser numérico. ", "");
                    Mensaje = Mensaje.Replace("* El campo Superficie Parcela es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie Parcela es obligatorio. ";
                    return false;
                }

                else if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie Parcela debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "SuperficieRegistralS")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Superficie registral-Sobre rasante debe ser numérico. ", "");
                    Mensaje = Mensaje.Replace("* El campo Superficie registral-Sobre rasante es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie registral-Sobre rasante es obligatorio. ";
                }

                 else   if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie registral-Sobre rasante debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "SuperficieRegistralB")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Superficie registral-Bajo rasante debe ser numérico. ", "");
                    Mensaje = Mensaje.Replace("* El campo Superficie registral-Bajo rasante es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie registral-Bajo rasante es obligatorio. ";
                    return false;
                }

                else if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie registral-Bajo rasante debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "NumPlazaRegistral")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Superficie registral-Número Plazas debe ser un entero. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !int.TryParse(proposedValue as String, out int numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie registral-Número Plazas debe ser un entero. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "SuperficieCatastralS")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Superficie catastral-Sobre rasante debe ser numérico. ", "");
                    Mensaje = Mensaje.Replace("* El campo Superficie catastral-Sobre rasante es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie catastral-Sobre rasante es obligatorio. ";
                }

                else if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie catastral-Sobre rasante debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "SuperficieCatastralB")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Superficie catastral-Bajo rasante debe ser numérico. ", "");
                    Mensaje = Mensaje.Replace("* El campo Superficie catastral-Bajo rasante es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie catastral-Bajo rasante es obligatorio. ";
                    return false;
                }

                else if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie catastral-Bajo rasante debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "NumPlazaCatastral")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Superficie catastral-Número Plazas debe ser un entero. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !int.TryParse(proposedValue as String, out int numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie catastral-Número Plazas debe ser un entero. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "SuperficieAlbaS")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Superficie ALBA-Sobre rasante debe ser numérico. ", "");
                    Mensaje = Mensaje.Replace("* El campo Superficie ALBA-Sobre rasante es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie ALBA-Sobre rasante es obligatorio. ";
                }

                else if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie ALBA-Sobre rasante debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "SuperficieAlbaB")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Superficie alba-Bajo rasante debe ser numérico. ", "");
                    Mensaje = Mensaje.Replace("* El campo Superficie alba-Bajo rasante es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie alba-Bajo rasante es obligatorio. ";
                    return false;
                }

                else if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie alba-Bajo rasante debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "NumPlazaAlba")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Superficie alba-Número Plazas debe ser un entero. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !int.TryParse(proposedValue as String, out int numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Superficie alba-Número Plazas debe ser un entero. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }
            return true;
        }
    } 
}

