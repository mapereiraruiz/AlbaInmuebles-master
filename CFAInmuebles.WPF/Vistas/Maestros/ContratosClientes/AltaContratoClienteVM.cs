using CFAInmuebles.Domain.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CFAInmuebles.Infrastructure.Data;
using System.Windows;
using System.ComponentModel;
using System.Reflection;

namespace CFAInmuebles.WPF
{
    public class AltaContratoClienteVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public ContratosClientes entity;
        
        private HomeContratoClienteVM baseVM;     
        private Inmuebles _inmueble;
        private Terceros _tercero;
        public AltaContratoClienteVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Contratos Clientes";
            }
        }

        public new Inmuebles Inmueble
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
                    entity.IdInmuebleNavigation = _inmueble;

                    Terceros = null;

                    var context = dbsALTAI.Where(m => m.Schema == "CONT_" + _inmueble.IdEmpresaNavigation.EmpresaALTAI).FirstOrDefault();

                    if (context != null)
                    {
                        try
                        {
                            Terceros = context.Terceros.Where(m => m.Cuenta.StartsWith("430")).ToList();
                        }

                        catch (Exception e)
                        { }

                    }
                    RaisePropertyChanged("Inmueble");
                }
            }
        }

        public new Terceros Tercero
        {
            get
            {
                CheckValidationState("Tercero", _tercero);
                return _tercero;
            }
            set
            {
                if (_tercero != value)
                {
                    CheckValidationState("Tercero", _tercero);
                    _tercero = value;
                    entity.NIF = _tercero.NIF;
                    entity.NombreCliente = _tercero.Nombre;
                    RaisePropertyChanged("Tercero");
                }
            }
        }
        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdContratoCliente == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).ToList();

            if (entity.IdContratoCliente > 0)
            {
                Inmueble = entity.IdInmuebleNavigation;

                var context = dbsALTAI.Where(m => m.Schema == "CONT_" + Inmueble.IdEmpresaNavigation.EmpresaALTAI).FirstOrDefault();

                if (context != null)
                {
                    try
                    {
                        Terceros = context.Terceros.Where(m => m.Cuenta.StartsWith("430")).ToList();
                        Tercero = Terceros.Where(m => m.Nombre == entity.NombreCliente).FirstOrDefault();
                    }

                    catch (Exception e)
                    { }

                }

                Trazabilidad("Maestros", "Contratos Clientes", entity.NumeroContrato.ToString(), "Consulta", "Ficha Contratos Clientes");
            }
            else
            {
                entity.FechaAlta = DateTime.Now;
                entity.FechaContrato = DateTime.Now;
                entity.FechaInicioFacturacion  = DateTime.Now;
            }

            //Comprobamos que inmuebles puede ver el usuario
            if (!UserId.Administrador)
            {
                var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();             
                Inmuebles = Inmuebles.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
            }

        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (baseVM.Errors.Count == 0)
            {
                entity.FechaSistema = DateTime.Now;
                entity.IdUsuarioNavigation = UserId;

                var model = db.ContratosClientes.Find(entity?.IdContratoCliente);

                if (model == null)
                {
                    db.ContratosClientes.Add(entity);
                    accion = "Alta";
                }
                else
                {
                    var newEntity = db.ContratosClientes.ApplyValues(entity, entity.IdContratoCliente);
                }

                db.SaveChanges();

                //Grabamos cambios
                if (accion == "Update")
                {
                    var historico = new HistoricoContratosClientes();
                    historico.IdContratoClienteNavigation = model;
                    historico.AgruparContrato = entity.AgruparContrato;
                    historico.ArchivoDigital = entity.ArchivoDigital;
                    historico.Comentario = entity.Comentario;
                    historico.CuentaBancaria = entity.CuentaBancaria;
                    historico.CuentaFianza = entity.CuentaFianza;
                    historico.DireccionEnvio = entity.DireccionEnvio;
                    historico.FechaAlta = entity.FechaAlta;
                    historico.FechaContrato = entity.FechaContrato;
                    historico.FechaEliminacion = entity.FechaEliminacion;
                    historico.FechaInicioFacturacion = entity.FechaInicioFacturacion;
                    historico.FechaUltimaFacturacion = entity.FechaUltimaFacturacion;                   
                    historico.FechaSistema = DateTime.Now;
                    historico.IdUsuarioNavigation = entity.IdUsuarioNavigation;
                    historico.IdConceptoFacturacionNavigation = entity.IdConceptoFacturacionNavigation;
                    historico.IdFormaPagoNavigation = entity.IdFormaPagoNavigation;
                    historico.IdInmuebleNavigation = entity.IdInmuebleNavigation;
                    historico.IdSwiftNavigation = entity.IdSwiftNavigation;
                    historico.IdTipoIpcNavigation = entity.IdTipoIpcNavigation;
                    historico.ImporteAval = entity.ImporteAval;
                    historico.Nif = entity.NIF;
                    historico.NumeroContrato = entity.NumeroContrato;
            
                    db.HistoricoContratosClientes.Add(historico);
                    db.SaveChanges();
                }

                Trazabilidad("Maestros", "Contratos Clientes", entity.NumeroContrato.ToString(), accion, "Ficha Contratos Clientes");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Contratos Clientes").FirstOrDefault());
            }

            else
                Mensaje = baseVM.Mensaje;
        }
        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.ContratosClientes.Find(entity.IdContratoCliente);

            //Comprobamos que no tenga ninguna entidad relacionada

            var datosimprimir = db.DatosImprimirFactura.Where(m => m.IdContratoCliente == entity.IdContratoCliente).Any();
            var facturacion = db.Facturacion.Where(m => m.IdContratoCliente == entity.IdContratoCliente && m.FechaEliminacion == null).Any();
            var historicofacturacion = db.HistoricoFacturacion.Where(m => m.IdContratoCliente == entity.IdContratoCliente).Any();
            var historicofacturacionbanco = db.HistoricoFacturacionBanco.Where(m => m.IdContratocliente == entity.IdContratoCliente).Any();

            if (!datosimprimir && !facturacion && !historicofacturacion && !historicofacturacionbanco)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Contratos Clientes").FirstOrDefault();
                viewmodel = new DeleteContratoClienteVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else

            {
                Mensaje = String.Empty;

                if (datosimprimir)
                    Mensaje = "Desvincule los Datos a Imprimir vinculados a este Contrato Cliente para poder eliminarlo." + Environment.NewLine;
                if (facturacion)
                    Mensaje += "Desvincule las Facturaciones vinculadas a este Contrato Cliente para poder eliminarlo." + Environment.NewLine;
                if (historicofacturacion)
                    Mensaje += "Desvincule los Históricos de Facturación vinculados a este Contrato Cliente para poder eliminarlo." + Environment.NewLine;
                if (historicofacturacionbanco)
                    Mensaje += "Desvincule laos Históricos de Facturación de Banco vinculados a este Contrato Cliente para poder eliminarlo." + Environment.NewLine;

            }
        }
        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Contratos Clientes").FirstOrDefault());
        }
        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            baseVM.entity = entity;
            baseVM.CheckValidationState(propertyName, proposedValue);
           
            if (propertyName == "Inmueble")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Inmueble es obligatorio.");       
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Tercero")
            {
                var cliente = proposedValue as Terceros;
                var existe = false;

                if (cliente != null)
                    existe = db.ContratosClientes.Where(m => m.NombreCliente == cliente.Nombre && m.FechaEliminacion == null).Any();

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Cliente es obligatorio.");                 
                    return false;
                }

                else if (existe)
                {
                    var contrato = db.ContratosClientes.Where(m => m.NombreCliente == cliente.Nombre && m.FechaEliminacion == null).FirstOrDefault();

                    if (contrato.IdContratoCliente != entity.IdContratoCliente)
                    {
                        SetError(propertyName, "Ya existe un Cliente con ese valor.");
                        return false;
                    }
                    else
                    {
                        SetError(propertyName, String.Empty);
                        return true;
                    }
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

