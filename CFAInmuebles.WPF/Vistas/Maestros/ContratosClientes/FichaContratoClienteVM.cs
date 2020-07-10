using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaContratoClienteVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;     
        private HomeContratoClienteVM baseVM;
        public FichaContratoClienteVM(HomeContratoClienteVM baseVM, ContratosClientes entidad = null)
        {

            entity = new ContratosClientes();

            if (entidad != null)
            {
                var properties = typeof(ContratosClientes).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
            PageViewModels.Add(new AltaContratoClienteVM(this.baseVM, entity));
            PageViewModels.Add(new AltaContratoClienteIdentificacionVM(this.baseVM, entity));
            PageViewModels.Add(new AltaContratoClienteDatosFacturacionVM(this.baseVM, entity));
            PageViewModels.Add(new AltaContratoClienteAvalFianzaVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoClienteFacturasVM(this, entity));
            PageViewModels.Add(new ContratoClienteTareasVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoClienteIncidenciasVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoClienteArticulosVM(this.baseVM, this.entity));
            PageViewModels.Add(new ContratoClienteHistorialOcupacionVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoClientePeriodosBajaVM(this, entity));
            PageViewModels.Add(new ContratoClienteDatosImprimirVM(this, entity));
            PageViewModels.Add(new ContratoClienteObrasVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoClienteAvalFianzaVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoClienteContactosVM(this, entity));
            PageViewModels.Add(new AltaContactoContratoClienteVM(this, null));
            PageViewModels.Add(new ContratoClienteHistoricoVM(this, entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Contratos Clientes";
            }
        }
        public IPageViewModel AltaContratoCliente
        {
            get { return Acceso("Alta Contratos Clientes", false); }
        }

        public IPageViewModel AltaContratoClienteIdentificacion
        {
            get { return Acceso("Alta Contratos Clientes Identificación", false); }
        }

        public IPageViewModel AltaContratoClienteDatosFacturacion
        {
            get { return Acceso("Alta Contratos Clientes Datos Facturación", false); }
        }

        public IPageViewModel AltaContratoClienteAvalFianza
        {
            get { return Acceso("Alta Contratos Clientes Aval Fianza", false); }
        }
        public IPageViewModel ContratoClienteFacturas
        {
            get { return Acceso("Contratos Clientes Facturas", false); }
        }
        public IPageViewModel ContratoClienteTareas
        {
            get { return Acceso("Contratos Clientes Tareas", false); }
        }

        public IPageViewModel ContratoClienteIncidencias
        {
            get { return Acceso("Contratos Clientes Incidencias", false); }
        }
        public IPageViewModel ContratoClienteArticulos
        {
            get { return Acceso("Contratos Clientes Artículos", false); }
        }

        public IPageViewModel ContratoClienteHistorialOcupacion
        {
            get { return Acceso("Contratos Clientes Historial Ocupación", false); }
        }
        public IPageViewModel ContratoClientePeriodosBaja
        {
            get { return Acceso("Contratos Clientes Periodos Baja", false); }
        }

        public IPageViewModel ContratoClienteDatosImprimir
        {
            get { return Acceso("Contratos Clientes Datos Imprimir", false); }
        }
        public IPageViewModel ContratoClienteObras
        {
            get { return Acceso("Contratos Clientes Obras", false); }
        }
        public IPageViewModel ContratoClienteAvalFianza
        {
            get { return Acceso("Contratos Clientes Aval y Fianzas", false); }
        }

        public IPageViewModel ContratoClienteContactos
        {
            get { return Acceso("Contratos Clientes Contactos", false); }
        }

        public IPageViewModel AltaContactoContratoCliente
        {
            get { return Acceso("Alta Contacto Contrato Cliente", false); }
        }
        public IPageViewModel ContratoClienteHistorico
        {
            get { return Acceso("Contrato Cliente Histórico Modificaciones", false); }
        }
        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Contratos Clientes").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }

    }
}
