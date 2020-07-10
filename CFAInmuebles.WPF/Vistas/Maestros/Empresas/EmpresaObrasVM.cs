using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class EmpresaObrasVM : ObservableViewModel, IPageViewModel
    {
        public Empresas entity;

        protected new ICommand _modifyCommand;
        protected ICommand _modifyCommandSelect;

        private FichaEmpresasVM baseVM;
        private HistorialObra _selectedItem;
        public EmpresaObrasVM(FichaEmpresasVM baseVM, Empresas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
    
        }
        public string Name
        {
            get
            {
                return "Empresa Obras";
            }
        }

        public HistorialObra SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistorialObra)p));
                }
                return _modifyCommand;
            }
        }

        public ICommand ModifyCommandSelect
        {
            get
            {
                if (_modifyCommandSelect == null)
                {
                    _modifyCommandSelect = new RelayCommand(p => ModifyData((HistorialObra)p));
                }
                return _modifyCommandSelect;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdEmpresa > 0)
            {
                var ficheroobras = db.ObrasFichero.Where(m => m.IdFichero == entity.IdEmpresa && m.IdTipoFicheroNavigation.Valor == "Empresa").Select(m => m.IdHistorialObra).ToList();
                Obras = db.HistorialObra.Where(m => m.FechaEliminacion == null && ficheroobras.Contains(m.IdHistorialObra)).ToList();
                Trazabilidad("Maestros", "Empresas", entity.Empresa, "Consulta", "Mantenimiento Empresas Obras");    
            }
        }
        protected void ModifyData(HistorialObra entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Empresa Obras").FirstOrDefault();
            viewmodel = new AltaEmpresaObrasVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}