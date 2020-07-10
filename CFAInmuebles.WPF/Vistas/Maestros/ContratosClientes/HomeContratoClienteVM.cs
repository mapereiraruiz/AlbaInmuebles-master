using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeContratoClienteVM : ObservableViewModel, IPageViewModel, IWindow
    {
        public ContratosClientes entity;
        public HomeContratoClienteVM()
        {
            PageViewModels.Add(new MantenimientoContratoClienteVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Contratos Clientes";

        }

        public string Name
        {
            get
            {
                return "Home Contratos Clientes";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeContratoCliente";
            }
        }

        public IPageViewModel MantenimientoContratoCliente
        {
            get { return Acceso("Mantenimiento Contratos Clientes", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Mantenimiento Contratos Clientes").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }

        public bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "NumeroContrato")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Referencia Contrato es obligatorio. ", "");
                    Mensaje = Mensaje.Replace("* El campo Referencia Contrato debe ser un entero. ", "");
                }

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Referencia Contrato es obligatorio.");
                    Mensaje += "* El campo Referencia Contrato es obligatorio. ";
                }


                if (!String.IsNullOrEmpty(proposedValue as String) && !int.TryParse(proposedValue as String, out int numValue))
                {
                    SetError(propertyName, "El campo Referencia Contrato debe ser numérico.");
                    Mensaje += "* El campo Referencia Contrato debe ser un entero. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Inmueble")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Inmueble es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Inmueble es obligatorio.");
                    Mensaje += "* El campo Inmueble es obligatorio. ";
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
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Cliente es obligatorio. ", "");
                    Mensaje = Mensaje.Replace("* Ya existe un Cliente con ese valor. ", "");
                }


                var cliente = proposedValue as Terceros;
                var existe = false;

                if (cliente != null)
                    existe = db.ContratosClientes.Where(m => m.NIF == cliente.NIF && m.FechaEliminacion == null).Any();

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Cliente es obligatorio.");
                    Mensaje += "* El campo Cliente es obligatorio. ";
                    return false;
                }

                else if (existe)
                {
                    var contrato = db.ContratosClientes.Where(m => m.NIF == cliente.NIF && m.FechaEliminacion == null).FirstOrDefault();

                    if (contrato.IdContratoCliente != entity.IdContratoCliente)
                    {
                        SetError(propertyName, "*");
                        Mensaje += "* Ya existe un Cliente con ese valor. ";
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

            if (propertyName == "FechaAlta")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Fecha Alta es obligatorio. ", "");

                if (proposedValue == null)
                {
                    Mensaje += "* El campo Fecha Alta es obligatorio. ";
                    SetError(propertyName, "El campo Fecha Alta es obligatorio");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "FechaInicioFacturacion")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Fecha Inicio Facturación es obligatorio. ", "");

                if (proposedValue == null)
                {
                    Mensaje += "* El campo Fecha Inicio Facturación es obligatorio. ";
                    SetError(propertyName, "El campo Fecha Inicio Facturación es obligatorio");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ImporteAval")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Importe Aval debe ser numérico. ", "");


                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    Mensaje += "* El campo Importe Aval debe ser numérico. ";
                    SetError(propertyName, "El campo Importe Aval debe ser numérico.");
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
