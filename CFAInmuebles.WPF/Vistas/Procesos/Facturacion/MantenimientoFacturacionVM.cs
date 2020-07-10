using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoFacturacionVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeFacturacionVM baseVM;
        private Facturacion _selectedItem;

        private DateTime? _fecha;
        private int? _numcontrato;

        public string Name
        {
            get
            {
                return "Mantenimiento Facturación";
            }
        }

        public MantenimientoFacturacionVM(HomeFacturacionVM baseVM)
        {
            this.baseVM = baseVM;
        }

      

        public Facturacion SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData((Facturacion)p));
                }
                return _modifyCommand;
            }
        }

        public DateTime? Fecha
        {
            get { return _fecha; }
            set
            {
                _fecha = value;
                RaisePropertyChanged("Fecha");
            }
        }

        public int? NumeroContrato
        {
            get { return _numcontrato; }
            set
            {
                _numcontrato = value;
                RaisePropertyChanged("NumeroContrato");
            }
        }


        protected override void LoadData()
        {
            base.LoadData();

            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });

            ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).ToList();

            //Comprobamos que contratos puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                ContratosClientes = ContratosClientes.Where(m => inmuebles.Contains(m.IdInmueble)).ToList();
            }

            ContratosClientes.Insert(0, new ContratosClientes { NombreCliente = "Seleccione:" });

            Trazabilidad("Procesos", "Facturación", "", "Consulta", "Mantenimiento Facturación");

            SearchData();
        }

        protected void ModifyData(Facturacion entity)
        {
            //var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Artículos").FirstOrDefault();
            //viewmodel = new FichaArticulosVM(baseVM, entity);
            //baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Procesos", "Facturación", "", "Búsqueda", "Cadena de consulta: Número Contrato=" + NumeroContrato + "&Empresa=" + Empresa?.Empresa
                + "&Fecha Factura=" + Fecha + "&Cliente=" + ContratoCliente?.NombreCliente);


            var search = db.Facturacion.Where(m => m.FechaEliminacion == null).AsQueryable();

            //Comprobamos que facturas puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                var contratos = db.ContratosClientes.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).Select(m => m.IdContratoCliente).ToList();
                search = search.Where(m => contratos.Contains(m.IdContratoCliente));
            }

            if (Fecha != null)
            {
                var endDate = Fecha?.AddDays(1);
                search = search.Where(m => m.Fecha >= Fecha && m.Fecha < endDate);
            }

            if (NumeroContrato != null)
                search = search.Where(m => m.IdContratoClienteNavigation.NumeroContrato == NumeroContrato);

            if (Empresa != null && Empresa.Empresa != "Seleccione:")
            {
                search = search.Where(m => m.IdContratoClienteNavigation.IdEmpresaNavigation == Empresa);
            }

            if (ContratoCliente != null && ContratoCliente.NombreCliente != "Seleccione:")
            {
                search = search.Where(m => m.IdContratoClienteNavigation.NombreCliente == ContratoCliente.NombreCliente);
            }

            Facturas = search.ToList();
        }
    }
}
