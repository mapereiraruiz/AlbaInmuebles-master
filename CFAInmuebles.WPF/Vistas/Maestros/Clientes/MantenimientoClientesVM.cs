using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoClientesVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeClientesVM baseVM;
        private Terceros _selectedItem;
        private string _cliente;
        private string _nif;
        private bool? _eliminado;
        private Empresas _empresa;


        public string Name
        {
            get
            {
                return "Mantenimiento Clientes";
            }
        }

        public MantenimientoClientesVM(HomeClientesVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string Cliente
        {
            get { return _cliente; }
            set
            {
                if (_cliente != value)
                {
                    _cliente = value;
                    RaisePropertyChanged("Cliente");
                }
            }
        }
        public new Empresas Empresa
        {
            get { return _empresa; }
            set
            {
                if (_empresa != value)
                {
                    Terceros = null;
                    NIF = null;
                    Cliente = null;
                    _empresa = value;
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


        public string NIF
        {
            get { return _nif; }
            set
            {
                if (_nif != value)
                {
                    _nif = value;
                    RaisePropertyChanged("NIF");
                }
            }
        }

        public bool?  Eliminado
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


        public Terceros SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Terceros)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });

            SearchData();
            Trazabilidad("Maestros", "Clientes", "", "Consulta", "Mantenimiento Clientes");

        }

        protected void ModifyData(Terceros entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Clientes").FirstOrDefault();
            viewmodel = new FichaClientesVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            if (_empresa != null)
            {
                Trazabilidad("Maestros", "Clientes", "", "Búsqueda", "Cadena de consulta: Empresa=" + Empresa + "&Cliente=" + Cliente + "&Nif=" + NIF + "&Eliminado=" + Eliminado);

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
                var search = Terceros.AsQueryable();

                if (!String.IsNullOrEmpty(Cliente))
                    search = search.Where(m => m.Nombre.Contains(Cliente));

                if (!String.IsNullOrEmpty(NIF))
                    search = search.Where(m => m.NIF.Contains(NIF));


                Terceros = search.ToList();
            }
        }
    }
}
