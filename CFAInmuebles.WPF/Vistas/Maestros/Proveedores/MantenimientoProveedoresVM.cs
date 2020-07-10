using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using DevExpress.Xpf.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoProveedoresVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;
        protected new ICommand _searchCommand;

        private HomeProveedoresVM baseVM;
        private Terceros _selectedItem;
        private Empresas _empresa;
        private List<Terceros> _terceros;
        private string _proveedor;
        public string _nif;
        
        public  string Name
        {
            get
            {
                return "Mantenimiento Proveedores";
            }
        }

        public MantenimientoProveedoresVM(HomeProveedoresVM baseVM)
        {
            this.baseVM = baseVM;          
        }

        public new string Proveedor
        {
            get { return _proveedor; }
            set
            {
                if (_proveedor != value)
                {
                    _proveedor = value;
                    RaisePropertyChanged("Proveedor");
                }
            }
        }

        public new string NIF
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
        public new Empresas Empresa
        {
            get { return _empresa; }
            set
            {
                if (_empresa != value)
                {
                    Terceros = null;
                    NIF = null;
                    Proveedor = null;
                    _empresa = value;
                    var context = dbsALTAI.Where(m => m.Schema == "CONT_" + _empresa.EmpresaALTAI).FirstOrDefault();

                    if (context != null)
                    {
                        try
                        {
                            Terceros = context.Terceros.Where(m => m.Cuenta.StartsWith("400") || m.Cuenta.StartsWith("410")).ToList();
                        }

                        catch (Exception e)
                        { }

                    }
                    RaisePropertyChanged("Empresa");
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

        public List<Terceros> Terceros
        {
            get { return _terceros; }
            set
            {
                _terceros = value;
                RaisePropertyChanged("Terceros");
            }
        }

        protected override void LoadData()
        {
            base.LoadData();
            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });

            SearchData();
            Trazabilidad("Maestros", "Proveedores", "", "Consulta", "Mantenimiento Proveedores");
        }

        private void ModifyData(Terceros entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Proveedores").FirstOrDefault();
            viewmodel = new FichaProveedoresVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();

            if (_empresa != null)
            {
                Trazabilidad("Maestros", "Proveedores", "", "Búsqueda", "Cadena de consulta: Proveedor=" + Proveedor);

                var context = dbsALTAI.Where(m => m.Schema == "CONT_" + _empresa.EmpresaALTAI).FirstOrDefault();

                if (context != null)
                {
                    try
                    {
                        Terceros = context.Terceros.ToList();
                    }

                    catch (Exception e)
                    { }

                }
                var search = Terceros.AsQueryable();

                if (!String.IsNullOrEmpty(Proveedor))
                    search = search.Where(m => m.Nombre.Contains(Proveedor));

                Terceros = search.ToList();
            }
        }
    }
}
