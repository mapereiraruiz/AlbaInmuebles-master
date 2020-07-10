using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeContratoProveedorVM : ObservableViewModel, IPageViewModel, IWindow
    {
       
        public HomeContratoProveedorVM()
        {
            PageViewModels.Add(new MantenimientoContratoProveedorVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Contratos Proveedores";

        }

        public string Name
        {
            get
            {
                return "Home Contratos Proveedores";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeContratoProveedor";
            }
        }

        public IPageViewModel MantenimientoContratoProveedor
        {
            get { return Acceso("Mantenimiento Contratos Proveedores", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Mantenimiento Contratos Proveedores").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }

        public bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Inmueble")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Inmueble es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Inmueble es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ContratoProveedor")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Proveedor es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Proveedor es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ReferenciaContrato")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Referencia Contrato es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Referencia Contrato es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "CosteAnual")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Coste Anual debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Coste Anual debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ImporteFactura")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Importe Factura debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Importe Factura debe ser numérico. ";
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
