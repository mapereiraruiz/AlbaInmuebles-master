using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoEmpresasVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeEmpresasVM baseVM;
        private Empresas _selectedItem;
        private string _empresa;

        public string Name
        {
            get
            {
                return "Mantenimiento Empresas";
            }
        }

        public MantenimientoEmpresasVM(HomeEmpresasVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public new string Empresa
        {
            get { return _empresa; }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                    RaisePropertyChanged("Empresa");
                }
            }
        }
        public Empresas SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Empresas)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();
            SearchData();
            Trazabilidad("Maestros", "Empresas", "", "Consulta", "Mantenimiento Empresas");
        }

        protected void ModifyData(Empresas entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Empresas").FirstOrDefault();
            viewmodel = new FichaEmpresasVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Maestros", "Empresas", "", "Búsqueda", "Cadena de consulta: Empresa=" + Empresa);

            var search  = db.Empresas.Where(m => m.FechaEliminacion == null).AsQueryable();
            
            if (!String.IsNullOrEmpty(Empresa))
                search = search.Where(m => m.Empresa.Contains(Empresa));

            Empresas = search.ToList();
        }
    }
}
