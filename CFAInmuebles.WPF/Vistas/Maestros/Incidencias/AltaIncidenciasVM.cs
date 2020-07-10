using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Net.Mail;
using System.Net;
using System.Web;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection;
using CFAInmuebles.Infrastructure.Data;

namespace CFAInmuebles.WPF
{
    public class AltaIncidenciasVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Incidencias entity;
        protected ICommand _openDialogCommand;
        private HomeIncidenciasVM baseVM;

        public int _idincidencia;
        public string _idfichero;
        private ContratosProveedores _proveedor;
        private TipoFichero _tipofichero;
        public DateTime _fechaincidencia;
        public string _incidencia;
        public string _descripcion;
        public DateTime? _fecharesolucion;
        public string _descripcionresolucion;
        public bool _finalizada;
        public string _servicio;
        public bool _programado;
        public string _afecta;
        public string _lpd;
        private bool _selectedItem;
        private List<String> _idficherovalues;
        private String _idficherovalue;

        public AltaIncidenciasVM(HomeIncidenciasVM baseVM, Incidencias entidad = null)
        {
            entity = new Incidencias();

            if (entidad != null)
            {

                var properties = typeof(Incidencias).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
                return "Alta Incidencias";
            }
        }
      
        public new ContratosProveedores ContratoProveedor
        {
            get
            {
                CheckValidationState("ContratoProveedor", _proveedor);
                return _proveedor;
            }
            set
            {
                if (_proveedor != value)
                {
                    CheckValidationState("ContratoProveedor", _proveedor);
                    _proveedor = value;
                    entity.IdContratoProveedorNavigation = _proveedor;
                    RaisePropertyChanged("ContratoProveedor");
                }
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
                                entity.IdFichero = db.Inmuebles.Where(m => m.Inmueble == _idficherovalue).FirstOrDefault().IdInmueble;
                                break;
                            case "Artículo":
                                entity.IdFichero = db.Articulos.Where(m => m.Articulo == _idficherovalue).FirstOrDefault().IdArticulo;
                                break;
                          
                            case "Contrato Cliente":
                                entity.IdFichero = db.ContratosClientes.Where(m => m.NombreCliente == _idficherovalue).FirstOrDefault().IdContratoCliente;
                                break;
                            case "Contrato Proveedor":
                                    entity.IdFichero = db.ContratosProveedores.Where(m => m.ReferenciaContrato == _idficherovalue).FirstOrDefault().IdContratoProveedor;
                                    break;          
                            case "Empresa":
                                entity.IdFichero = db.Empresas.Where(m => m.Empresa == _idficherovalue).FirstOrDefault().IdEmpresa;
                                break;
                           
                            case "Obra":
                                entity.IdFichero = db.HistorialObra.Where(m => m.Descripcion == _idficherovalue).FirstOrDefault().IdHistorialObra;
                                break;
                            case "Incidencia":
                                entity.IdFichero = db.Incidencias.Where(m => m.Incidencia == _idficherovalue).FirstOrDefault().IdIncidencia;
                                break;
                            case "Mantenimiento":
                                entity.IdFichero = db.Mantenimientos.Where(m => m.IdTipoMantenimientoNavigation.Valor == _idficherovalue).FirstOrDefault().IdMantenimiento;
                                break;
                        }
                    }
                    
                    RaisePropertyChanged("IdFicheroValue");
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
                    entity.IdTipoFicheroNavigation = _tipofichero;

                    switch(_tipofichero?.Valor)
                    {
                        case "Inmueble":
                            IdFicheroValues = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.Inmueble).ToList();
                            break;
                        case "Artículo":
                            IdFicheroValues = db.Articulos.Where(m => m.FechaEliminacion == null).Select(m => m.Articulo).ToList();
                            break;                     
                        case "Contrato Cliente":
                            IdFicheroValues = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(m => m.NombreCliente).ToList();
                            break;
                        case "Contrato Proveedor":
                            IdFicheroValues = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(m => m.ReferenciaContrato).ToList();
                            break;
                        case "Empresa":
                            IdFicheroValues = db.Empresas.Where(m => m.FechaEliminacion == null).Select(m => m.Empresa).ToList();
                            break;
                        case "Obra":
                            IdFicheroValues = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(m => m.Descripcion).ToList();
                            break;
                        case "Incidencia":
                            IdFicheroValues = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(m => m.Incidencia).ToList();
                            break;
                        case "Mantenimiento":
                            IdFicheroValues = db.Mantenimientos.Where(m => m.FechaEliminacion == null).Select(m => m.IdTipoMantenimientoNavigation.Valor).ToList();
                            break;
                    }
                    
                    RaisePropertyChanged("TipoFichero");
                }
            }
        }

        public DateTime FechaIncidencia
        {
            get
            {
                CheckValidationState("FechaIncidencia", _fechaincidencia);
                return _fechaincidencia;
            }
            set
            {
                if (_fechaincidencia != value)
                {
                    CheckValidationState("FechaIncidencia", value);
                    _fechaincidencia = value;
                    entity.FechaIncidencia = _fechaincidencia;
                    RaisePropertyChanged("FechaIncidencia");
                }
            }
        }

        public string Incidencia
        {
            get
            {
                CheckValidationState("Incidencia", _incidencia);
                return _incidencia;
            }
            set
            {
                if (_incidencia != value)
                {
                    CheckValidationState("Incidencia", value);
                    _incidencia = value;
                    entity.Incidencia = _incidencia;
                    RaisePropertyChanged("Incidencia");
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
                    entity.Descripcion = _descripcion;
                    RaisePropertyChanged("Descripcion");
                }
            }
        }
        public DateTime? FechaResolucion
        {
            get { return _fecharesolucion; }
            set
            {
                if (_fecharesolucion != value)
                {
                    _fecharesolucion = value;
                    entity.FechaResolucion = _fecharesolucion;
                    RaisePropertyChanged("FechaResolucion");
                }
            }
        }
        public string DescripcionResolucion
        {
            get { return _descripcionresolucion; }
            set
            {
                if (_descripcionresolucion != value)
                {
                    _descripcionresolucion = value;
                    entity.DescripcionResolucion = _descripcionresolucion;
                    RaisePropertyChanged("DescripcionResolucion");
                }
            }
        }
        public bool Finalizada
        {
            get { return _finalizada; }
            set
            {
                if (_finalizada != value)
                {
                    _finalizada = value;
                    entity.Finalizada = _finalizada;
                    RaisePropertyChanged("Finalizada");
                }
            }
        }
        public string Servicio
        {
            get { return _servicio; }
            set
            {
                if (_servicio != value)
                {
                    _servicio = value;
                    entity.Servicio = _servicio;
                    RaisePropertyChanged("Servicio");
                }
            }
        }
        public bool Programado
        {
            get { return _programado; }
            set
            {
                if (_programado != value)
                {
                    _programado = value;
                    entity.Programado = _programado;
                    RaisePropertyChanged("Programado");
                }
            }
        }
        public string Afecta
        {
            get { return _afecta; }
            set
            {
                if (_afecta != value)
                {
                    _afecta = value;
                    entity.Afecta = _afecta;
                    RaisePropertyChanged("Afecta");
                }
            }
        }
        public string Lpd
        {
            get { return _lpd; }
            set
            {
                if (_lpd != value)
                {
                    _lpd = value;
                    entity.Lpd = _lpd;
                    RaisePropertyChanged("Lpd");
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

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdIncidencia == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;

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
        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.ToList();
            ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).DistinctBy(m => m.NombreProveedor).ToList();
            

            if (entity.IdIncidencia > 0)
            {
                
                switch (entity.IdTipoFicheroNavigation?.Valor)
                {
                    case "Inmueble":
                        IdFicheroValues = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.Inmueble).ToList();
                        IdFicheroValue = db.Inmuebles.Find(entity.IdFichero)?.Inmueble;
                        break;
                    case "Artículo":
                        IdFicheroValues = db.Articulos.Where(m => m.FechaEliminacion == null).Select(m => m.Articulo).ToList();
                        IdFicheroValue = db.Articulos.Find(entity.IdFichero)?.Articulo;
                        break;
                   
                    case "Contrato Cliente":
                        IdFicheroValues = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(m => m.NombreCliente).ToList();
                        IdFicheroValue = db.ContratosClientes.Find(entity.IdFichero)?.NombreCliente.ToString();
                        break;
                    case "Contrato Proveedor":
                        IdFicheroValues = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(m => m.ReferenciaContrato).ToList();
                        IdFicheroValue = db.ContratosProveedores.Find(entity.IdFichero)?.NombreProveedor.ToString();
                        break;
                    case "Empresa":
                        IdFicheroValues = db.Empresas.Where(m => m.FechaEliminacion == null).Select(m => m.Empresa).ToList();
                        IdFicheroValue = db.Empresas.Find(entity.IdFichero)?.Empresa;
                        break;                   
                    case "Obra":
                        IdFicheroValues = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(m => m.Descripcion).ToList();
                        IdFicheroValue = db.HistorialObra.Find(entity.IdFichero)?.Descripcion;
                        break;
                    case "Incidencia":
                        IdFicheroValues = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(m => m.Incidencia).ToList();
                        IdFicheroValue = db.Incidencias.Find(entity.IdFichero)?.Incidencia;
                        break;
                    case "Mantenimiento":
                        IdFicheroValues = db.Mantenimientos.Where(m => m.FechaEliminacion == null).Select(m => m.IdTipoMantenimientoNavigation.Valor).ToList();
                        IdFicheroValue = db.Mantenimientos.Find(entity.IdFichero)?.IdTipoMantenimientoNavigation.Valor;
                        break;
                }
               
                ContratoProveedor = entity.IdContratoProveedorNavigation;
                TipoFichero = entity.IdTipoFicheroNavigation;
                FechaIncidencia = entity.FechaIncidencia;
                Incidencia = entity.Incidencia;
                Descripcion = entity.Descripcion;
                FechaResolucion = entity.FechaResolucion;
                DescripcionResolucion = entity.DescripcionResolucion;
                Finalizada = entity.Finalizada;
                Servicio = entity.Servicio;
                Programado = entity.Programado;
                Afecta = entity.Afecta;
                Lpd = entity.Lpd;

                Trazabilidad("Maestros", "Incidencias", Incidencia, "Consulta", "Ficha Incidencias");
            }
            else
            {
                FechaIncidencia = DateTime.Now;
                FechaResolucion = DateTime.Now;
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

                var model = db.Incidencias.Find(entity?.IdIncidencia);

                if (model == null)
                {
                    db.Incidencias.Add(entity);
                    accion = "Alta";
                }
                else
                {
                    var newEntity = db.Incidencias.ApplyValues(entity, entity.IdIncidencia);
                }
 
                db.SaveChanges();
                Trazabilidad("Maestros", "Incidencias", Incidencia, accion, "Ficha Incidencias");

                //***** Cuando se crea una incidencia se envía un correo al contacto/os que tiene asignado el proveedor de la incidencia
                if (accion == "Alta")
                {
                    EnviarCorreo(entity.IdFichero, entity.IdContratoProveedor);
                }
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Incidencias").FirstOrDefault());
            }
        }


        protected void EnviarCorreo(int IdInmueble, int IdProveedor)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            var incidentEmailToContact_Subject = configuration["EmailConfiguration:IncidentEmailToContact_Subject"];
            var incidentEmailToContact_Body = HttpUtility.UrlDecode(configuration["EmailConfiguration:IncidentEmailToContact_Body"]);

            incidentEmailToContact_Body = incidentEmailToContact_Body.Replace("[INCIDENCIA]", Incidencia);
            incidentEmailToContact_Body = incidentEmailToContact_Body.Replace("[DESCRIPCION]", Descripcion);
            incidentEmailToContact_Body = incidentEmailToContact_Body.Replace("[PROVEEDOR]", ContratoProveedor.ToString());
            incidentEmailToContact_Body = incidentEmailToContact_Body.Replace("[FECHAINCIDENCIA]", FechaIncidencia.ToString());

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(configuration["EmailConfiguration:MailFrom"]);
            mail.Subject = incidentEmailToContact_Subject;
            mail.Body = incidentEmailToContact_Body;
            mail.IsBodyHtml = true;

            Contactos = db.Contactos.Where(m => (m.IdTipoFicheroNavigation.Valor == "Inmueble" && m.IdFichero == IdInmueble) || (m.IdTipoFicheroNavigation.Valor == "Contrato Proveedor" && m.IdFichero == IdProveedor)).ToList();

            foreach (var correo in Contactos)
            {
                try
                {
                    var emailContact = correo.Mail;

                    if (emailContact != null)
                    {
                        if (bool.Parse(configuration["EmailConfiguration:EsEntornoPruebas"]))
                            mail.To.Add(configuration["EmailConfiguration:MailToPruebas"]); 
                        else
                            mail.To.Add(emailContact);

                        var smtpClient = new SmtpClient(configuration["EmailConfiguration:MailServer"], Convert.ToInt32(configuration["EmailConfiguration:MailPort"]))
                        {
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(configuration["EmailConfiguration:MailUser"], configuration["EmailConfiguration:MailPass"]),
                            EnableSsl = bool.Parse(configuration["EmailConfiguration:MailSSL"]),
                        };

                        smtpClient.Send(mail);
                        Trazabilidad("Maestros", "Incidencias", emailContact, "Correo enviado al contacto con la incidencia creada", "Ficha Incidencias");
                        mail.To.Remove(mail.To[0]);
                    }
                }

                catch (SmtpException ex)
                {
                    Trazabilidad("Maestros", "Incidencias", correo.Mail, "Error al enviar el email al contacto", "Ficha Incidencias");
                }
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Incidencias").FirstOrDefault();
                viewmodel = new DeleteIncidenciasVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
        }


        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Incidencias").FirstOrDefault());
        }


        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "ContratoProveedor")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace(" * El campo Proveedor es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Proveedor es obligatorio. ";
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
                    Mensaje = Mensaje.Replace(" * El campo Tipo Fichero es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Tipo Fichero es obligatorio. ";
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
                {
                    Mensaje = Mensaje.Replace("* El campo Fichero es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Fichero es obligatorio. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Incidencia")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace(" * El campo Incidencia es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += " * El campo Incidencia es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "FechaIncidencia")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Fecha Incidencia es obligatorio. ", "");

                if (proposedValue == null)
                {
                    Mensaje += "* El campo Fecha Incidencia es obligatorio. ";
                    SetError(propertyName, "El campo Fecha Incidencia es obligatorio");
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

