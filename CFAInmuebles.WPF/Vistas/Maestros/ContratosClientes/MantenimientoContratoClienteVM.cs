using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoContratoClienteVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeContratoClienteVM baseVM;
        private ContratosClientes _selectedItem;
        private Empresas _empresa;
        private bool? _eliminado;

        public string Name
        {
            get
            {
                return "Mantenimiento Contratos Clientes";
            }
        }

        public MantenimientoContratoClienteVM(HomeContratoClienteVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public bool? Eliminado
        {
            get { return _eliminado; }
            set
            {
                if (_eliminado != value)
                {
                    _eliminado = value;
                    RaisePropertyChanged("Eliminado");
                }
            }
        }

        public ContratosClientes SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((ContratosClientes)p));
                }
                return _modifyCommand;
            }
        }

        public new Empresas Empresa
        {
            get
            {
                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;

                    Terceros = null;

                    var context = dbsALTAI.Where(m => m.Schema == "CONT_" + _empresa.EmpresaALTAI).FirstOrDefault();

                    if (context != null)
                    {
                        try
                        {
                            Terceros = context.Terceros.Where(m => m.Cuenta.StartsWith("430")).ToList();
                        }

                        catch (Exception e)
                        { }

                    }
                    RaisePropertyChanged("Empresa");
                }
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).ToList();

            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });

            ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).ToList();
            Trazabilidad("Maestros", "Contratos Clientes", "", "Consulta", "Mantenimiento Contratos Clientes");

            //Comprobamos que contratos puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                ContratosClientes = ContratosClientes.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                Inmuebles = Inmuebles.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
            }

            Inmuebles.Insert(0, new Inmuebles { Inmueble = "Seleccione:" });
            SearchData();
        }
     
        private void ModifyData(ContratosClientes entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Contratos Clientes").FirstOrDefault();
            viewmodel = new FichaContratoClienteVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Maestros", "Contratos Clientes", "", "Búsqueda", "Cadena de consulta: Empresa=" + Empresa?.Empresa + "&Inmueble=" + Inmueble?.Inmueble + "&Cliente=" + Tercero?.Nombre
               + "&Eliminado=" + Eliminado);

            var search = db.ContratosClientes.Where(m => m.FechaEliminacion == null).AsQueryable();

            //Comprobamos que contratos puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                search = search.Where(m => inmuebles.Contains(m.IdInmueble));
            }

            if (Empresa != null && Empresa.Empresa != "Seleccione:")
            {
                search = search.Where(m => m.IdInmuebleNavigation.IdEmpresaNavigation == Empresa);
            }

            if (Inmueble != null && Inmueble.Inmueble != "Seleccione:")
            {
                search = search.Where(m => m.IdInmuebleNavigation == Inmueble);
            }

            if (Tercero != null && Tercero.Nombre != "Seleccione:")
            {
                search = search.Where(m => m.NIF == Tercero.NIF);
            }

            if (Eliminado == true)
                search = search.Where(m => m.FechaEliminacion != null);
            else
                search = search.Where(m => m.FechaEliminacion == null);


            ContratosClientes = search.ToList();
        }
    }
}

