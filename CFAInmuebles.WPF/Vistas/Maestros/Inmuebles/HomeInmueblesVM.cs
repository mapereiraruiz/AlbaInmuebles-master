using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class HomeInmueblesVM : ObservableViewModel, IPageViewModel, IWindow
    {

        public Inmuebles entity;
        public HomeInmueblesVM()
        {
            PageViewModels.Add(new MantenimientoInmueblesVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Inmuebles";

        }

        public string Name
        {
            get
            {
                return "Home Inmuebles";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeInmuebles";
            }
        }

        public IPageViewModel MantenimientoInmuebles
        {
            get { return Acceso("Mantenimiento Inmuebles", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Mantenimiento Inmuebles").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }

        public bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
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

                else if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
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

            if (propertyName == "FechaCompra")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Fecha Compra es obligatorio");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ImporteCompra")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Importe Compra debe ser numérico. ", "");
                
                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Importe Compra debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ImporteVenta")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Importe Venta debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    Mensaje += "* El campo Importe Venta debe ser numérico. ";
                    SetError(propertyName, "El campo Importe Venta debe ser numérico.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "TasaCapitalizacion")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Tasa Capitalización debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    Mensaje += "* El campo Tasa Capitalización debe ser numérico. ";
                    SetError(propertyName, "El campo Tasa Capitalización debe ser numérico.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ValorPlazaGaraje")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Valor Plaza Garaje debe ser numérico. ", "");


                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    Mensaje += "* El campo Valor Plaza Garaje debe ser numérico. ";
                    SetError(propertyName, "El campo Valor Plaza Garaje debe ser numérico.");
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
