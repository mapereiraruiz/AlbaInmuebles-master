using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reflection;

namespace CFAInmuebles.WPF
{
    public class AltaObraVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
		public HistorialObra entitybase;
		public ObrasFichero entity;

		private HomeObraVM baseVM;

		public string _descripcion;
		public DateTime? _fechaapertura;
		public DateTime? _fechacierre;
		public string _numeroexpediente;
		private string _presupuestoadjudicacion;
		private string _costefinal;
		private string _costeasumido;
		public bool _direccionfacultativasino;
		public string _direccionfacultativa;
        private List<String> _idficherovalues;
        private String _idficherovalue;
        private TipoFichero _tipofichero;
        private List<String> _idnumerosexpedientes;
        private String _idnumeroexpediente;
        private bool? _tienelicencia;


        private bool _selectedItem;
		public AltaObraVM(HomeObraVM baseVM, HistorialObra entidad, ObrasFichero entity = null)
		{
            entitybase = new HistorialObra();

            if (entidad != null)
            {

                var properties = typeof(HistorialObra).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !(p.PropertyType.IsInterface && p.PropertyType.IsGenericType &&
                              p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                             && p.CanWrite).ToList();
                properties.ForEach(p =>
                {
                    p.GetValue(entidad);
                    p.SetValue(entitybase, p.GetValue(entidad));
                });
            }

            this.entity = entity;
            this.baseVM = baseVM;
		}
		public string Name
        {
            get
            {
                 return "Alta Obra";
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

        public bool? TieneLicencia
        {
            get { return _tienelicencia; }
            set
            {
                _tienelicencia = value;
                entitybase.TieneLicencia = _tienelicencia;

                if (_tienelicencia == true && _idficherovalue != null && TipoFichero?.Valor == "Inmueble")
                {
                    int? idFicheroInmueble = db.Inmuebles.Where(m => m.Inmueble == _idficherovalue).FirstOrDefault()?.IdInmueble;
                    NumerosExpedientes = db.Licencias.Where(m => m.IdTipoFicheroNavigation.Valor == "Inmueble" && m.IdFichero == idFicheroInmueble).Select(m => m.NumeroExpediente).ToList();
                }
                RaisePropertyChanged("TieneLicencia");
            }
        }

        public String IdFicheroValue
        {
            get
            {
                CheckValidationState("IdFicheroValue", _idficherovalue);
                return _idficherovalue;
            }
            set
            {
                if (_idficherovalue != value)
                {
                    CheckValidationState("IdFicheroValue", _idficherovalue);
                    _idficherovalue = value;

                    if (_idficherovalue != null)
                    {
                        switch (_tipofichero?.Valor)
                        {
                            case "Inmueble":
                                entitybase.IdFichero = db.Inmuebles.Where(m => m.Inmueble == _idficherovalue).FirstOrDefault().IdInmueble;
                                break;
                            case "Artículo":
                                entitybase.IdFichero = db.Articulos.Where(m => m.Articulo == _idficherovalue).FirstOrDefault().IdArticulo;
                                break;
                            case "Empresa":
                                entitybase.IdFichero = db.Empresas.Where(m => m.Empresa == _idficherovalue).FirstOrDefault().IdEmpresa;
                                break;
                            case "Contrato Cliente":
                                entitybase.IdFichero = db.ContratosClientes.Where(m => m.NombreCliente == _idficherovalue).FirstOrDefault().IdContratoCliente;
                                break;

                        }
                    }

                    RaisePropertyChanged("IdFicheroValue");
                }
            }
        }
        public List<String> IdFicheroValues
        {

            get { return _idficherovalues; }
            set
            {
                if (_idficherovalues != value)
                {
                    _idficherovalues = value;
                    RaisePropertyChanged("IdFicheroValues");
                }
            }
        }

        public String IdNumeroExpediente
        {
            get
            {        
                return _idnumeroexpediente;
            }
            set
            {
                if (_idnumeroexpediente != value)
                {                    
                    _idnumeroexpediente = value;
                    RaisePropertyChanged("IdNumeroExpediente");
                }
            }
        }
        public List<String> NumerosExpedientes
        {

            get { return _idnumerosexpedientes; }
            set
            {
                if (_idnumerosexpedientes != value)
                {
                    _idnumerosexpedientes = value;
                    RaisePropertyChanged("NumerosExpedientes");
                }
            }
        }
        public new TipoFichero TipoFichero
        {
            get
            {
                CheckValidationState("TipoFichero", _tipofichero);
                return _tipofichero;
            }
            set
            {
                if (_tipofichero != value)
                {
                    CheckValidationState("TipoFichero", _tipofichero);
                    _tipofichero = value;
                    entitybase.IdTipoFicheroNavigation = _tipofichero;

                    switch (_tipofichero?.Valor)
                    {
                        case "Inmueble":
                            IdFicheroValues = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.Inmueble).ToList();
                            break;
                        case "Artículo":
                            IdFicheroValues = db.Articulos.Where(m => m.FechaEliminacion == null).Select(m => m.Articulo).ToList();
                            break;
                        case "Empresa":
                            IdFicheroValues = db.Empresas.Where(m => m.FechaEliminacion == null).Select(m => m.Empresa).ToList();
                            break;
                        case "Contrato Cliente":
                            IdFicheroValues = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(m => m.NombreCliente).ToList();
                            break;
                    }

                    RaisePropertyChanged("TipoFichero");
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
                    entitybase.Descripcion = _descripcion;
                    RaisePropertyChanged("Descripcion");
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
                    entitybase.FechaApertura = _fechaapertura;
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
                    entitybase.FechaCierre = _fechacierre;
                    RaisePropertyChanged("FechaCierre");
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
                    entitybase.NumeroExpediente = _numeroexpediente;
                    RaisePropertyChanged("NumeroExpediente");
				}
			}
		}

		public string PresupuestoAdjudicacion
		{
			get
			{
				CheckValidationState("PresupuestoAdjudicacion", _presupuestoadjudicacion);
				return _presupuestoadjudicacion;
			}
			set
			{
				if (_presupuestoadjudicacion != value)
				{
					CheckValidationState("PresupuestoAdjudicacion", _presupuestoadjudicacion);
					_presupuestoadjudicacion = value;
                    if (decimal.TryParse(PresupuestoAdjudicacion, out decimal numValue))
                        entitybase.PresupuestoAdjudicacion = decimal.Parse(PresupuestoAdjudicacion);
                    RaisePropertyChanged("PresupuestoAdjudicacion");
				}
			}
		}

		public string CosteFinal
		{
			get
			{
				CheckValidationState("CosteFinal", _costefinal);
				return _costefinal;
			}
			set
			{
				if (_costefinal != value)
				{
					CheckValidationState("CosteFinal", _costefinal);
					_costefinal = value;
                    if (decimal.TryParse(CosteFinal, out decimal numValue))
                        entitybase.CosteFinal = decimal.Parse(CosteFinal);

                    RaisePropertyChanged("CosteFinal");
				}
			}
		}

		public string CosteAsumido
		{
			get
			{
				CheckValidationState("CosteAsumido", _costeasumido);
				return _costeasumido;
			}
			set
			{
				if (_costeasumido != value)
				{
					CheckValidationState("CosteAsumido", _costeasumido);
					_costeasumido = value;
                    if (decimal.TryParse(CosteAsumido, out decimal numValue))
                        entitybase.CosteAsumido = decimal.Parse(CosteAsumido);

                    RaisePropertyChanged("CosteAsumido");
				}
			}
		}

		public bool DireccionFacultativaSino
		{
			get { return _direccionfacultativasino; }
			set
			{
				if (_direccionfacultativasino != value)
				{
					_direccionfacultativasino = value;
                    entitybase.DireccionFacultativaSino = _direccionfacultativasino;
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
                    entitybase.DireccionFacultativa = _direccionfacultativa;
                    RaisePropertyChanged("DireccionFacultativa");
				}
			}
		}



        public Visibility MostrarBotones
        {
            get
            {
				if (entitybase.IdHistorialObra == 0)
				{
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();
			
			TipoFicheros = db.TipoFichero.Where(m => m.Valor == "Artículo" || m.Valor == "Inmueble" || m.Valor == "Empresa" || m.Valor == "Contrato Cliente").ToList();
            TipoFicheros.Insert(0, new TipoFichero { Valor = "Seleccione:" });
            TipoObras = db.TipoObra.ToList();
            TipoObras.Insert(0, new TipoObra { Valor = "Seleccione:" });

            ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).DistinctBy(m => m.NombreProveedor).ToList();

            if (entitybase.IdHistorialObra > 0)
            {
				TipoFichero = entitybase.IdTipoFicheroNavigation;
				TipoObra = entitybase.IdTipoObraNavigation;               
                ContratoProveedor = entitybase.IdContratoProveedorNavigation;
				Descripcion = entitybase.Descripcion;
				FechaApertura = entitybase.FechaApertura;
				FechaCierre = entitybase.FechaCierre;
                NumeroExpediente = entitybase.NumeroExpediente;
				PresupuestoAdjudicacion = entitybase.PresupuestoAdjudicacion.ToString();
				CosteFinal = entitybase.CosteFinal.ToString();
				CosteAsumido = entitybase.CosteAsumido.ToString();
				DireccionFacultativaSino = entitybase.DireccionFacultativaSino;
				DireccionFacultativa = entitybase.DireccionFacultativa;
                TieneLicencia = entitybase.TieneLicencia;

                switch (entitybase.IdTipoFicheroNavigation?.Valor)
                {
                    case "Inmueble":
                        IdFicheroValues = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.Inmueble).ToList();
                        IdFicheroValue = db.Inmuebles.Find(entitybase.IdFichero)?.Inmueble;
                        break;
                    case "Artículo":
                        IdFicheroValues = db.Articulos.Where(m => m.FechaEliminacion == null).Select(m => m.Articulo).ToList();
                        IdFicheroValue = db.Articulos.Find(entitybase.IdFichero)?.Articulo;
                        break;
                    case "Empresa":
                        IdFicheroValues = db.Empresas.Where(m => m.FechaEliminacion == null).Select(m => m.Empresa).ToList();
                        IdFicheroValue = db.Empresas.Find(entitybase.IdFichero)?.Empresa;
                        break;
                    case "Contrato Cliente":
                        IdFicheroValues = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(m => m.NombreCliente).ToList();
                        IdFicheroValue = db.ContratosClientes.Find(entitybase.IdFichero)?.NombreCliente;
                        break;

                }

                Trazabilidad("Mantenimientos", "Obras", Descripcion, "Consulta", "Ficha Obra");
			}
            else
            {
				FechaApertura = DateTime.Now;
				FechaCierre = DateTime.Now;	
				DireccionFacultativaSino = false;				
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
			string accion = "Update";

			if (Errors.Count == 0)
            {
                var model = db.HistorialObra.Find(entitybase?.IdHistorialObra);
              
                if (model == null)
                { 
                    model = new HistorialObra();
                    db.HistorialObra.Add(model);
					accion = "Alta";
				}

				model.IdTipoFicheroNavigation = TipoFichero;
				model.IdTipoObraNavigation = TipoObra;
				model.IdContratoProveedorNavigation = ContratoProveedor;
				model.Descripcion = Descripcion;
				model.FechaApertura = FechaApertura;
				model.FechaCierre = FechaCierre;
                model.TieneLicencia = TieneLicencia;


				if (!String.IsNullOrEmpty(PresupuestoAdjudicacion))
					model.PresupuestoAdjudicacion = decimal.Parse(PresupuestoAdjudicacion);

				if (!String.IsNullOrEmpty(CosteFinal))
					model.CosteFinal = decimal.Parse(CosteFinal);

				if (!String.IsNullOrEmpty(CosteAsumido))
					model.CosteAsumido = decimal.Parse(CosteAsumido);

				model.DireccionFacultativaSino = DireccionFacultativaSino;
				model.DireccionFacultativa = DireccionFacultativa;
                model.IdFichero = entitybase.IdFichero;
                model.IdUsuarioNavigation = UserId;
				model.FechaSistema = DateTime.Now;

                if (model.TieneLicencia != null && model.TieneLicencia == true)
                {
                    model.NumeroExpediente = db.Licencias.Where(m => m.IdTipoFicheroNavigation.Valor == "Inmueble" && m.IdFichero == model.IdFichero).FirstOrDefault()?.NumeroExpediente;
                }

                if (accion == "Alta")
                {
					db.SaveChanges();
					var ficheroobra = new ObrasFichero();
					ficheroobra.IdHistorialObra = model.IdHistorialObra;
					ficheroobra.IdTipoFicheroNavigation = model.IdTipoFicheroNavigation;
                    ficheroobra.IdFichero = model.IdFichero;				
					db.ObrasFichero.Add(ficheroobra);
				}
                else
                {
                    var updateobra = db.ObrasFichero.Where(m => m.IdHistorialObra == model.IdHistorialObra).FirstOrDefault();
                    
                    if (updateobra != null)
                    {
                        updateobra.IdFichero = model.IdFichero;
                        updateobra.IdTipoFicheroNavigation = model.IdTipoFicheroNavigation;
                    }
                }


                db.SaveChanges();
				Trazabilidad("Mantenimientos", "Obras", Descripcion, accion, "Ficha Obra");
				baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Obra").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entitybase != null)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Obra").FirstOrDefault();
				viewmodel = new DeleteObraVM(baseVM, entitybase);
				baseVM.CurrentPageViewModel = viewmodel;
            }
        }


        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Obra").FirstOrDefault());
        }



		protected bool CheckValidationState<T>(string propertyName, T proposedValue)
		{

			if (propertyName == "PresupuestoAdjudicacion")
			{
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Presupuesto Adjudicación debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
				{
                    Mensaje += "* El campo Presupuesto Adjudicación debe ser numérico. ";
                    SetError(propertyName, "*");
					return false;
				}

				else
				{
					SetError(propertyName, String.Empty);
					return true;
				}
			}

			if (propertyName == "CosteFinal")
			{
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Coste Final debe ser numérico. ", "");


                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
				{
                    Mensaje += "* El campo Coste Final debe ser numérico. ";
					SetError(propertyName, "*");
					return false;
				}

				else
				{
					SetError(propertyName, String.Empty);
					return true;
				}
			}

			if (propertyName == "CosteAsumido")
			{
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Coste Asumido debe ser numérico. ", "");


                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
				{
                    Mensaje += "* El campo Coste Asumido debe ser numérico. ";
                    SetError(propertyName, "*");
					return false;
				}

				else
				{
					SetError(propertyName, String.Empty);
					return true;
				}
			}

            if (propertyName == "TipoFichero")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Tipo Fichero es obligatorio. ", "");


                if (proposedValue == null)
                {
                    Mensaje += "* El campo Tipo Fichero es obligatorio. ";
                    SetError(propertyName, "*");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "IdFicheroValue")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Fichero es obligatorio. ", "");


                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    Mensaje += "* El campo Fichero es obligatorio. ";
                    SetError(propertyName, "*");
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
