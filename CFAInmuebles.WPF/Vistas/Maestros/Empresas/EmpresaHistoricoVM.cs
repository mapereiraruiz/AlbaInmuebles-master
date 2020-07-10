using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class EmpresaHistoricoVM : ObservableViewModel, IPageViewModel
    {
        public Empresas entity;

        protected new ICommand _modifyCommand;
       

        private FichaEmpresasVM baseVM;
        private HistoricoEmpresas _selectedItem;
        public EmpresaHistoricoVM(FichaEmpresasVM baseVM, Empresas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Empresa Histórico Modificaciones";
            }
        }

        public HistoricoEmpresas SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistoricoEmpresas)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdEmpresa > 0)
            {
                HistoricoEmpresas = db.HistoricoEmpresas.Where(m => m.FechaEliminacion == null &&  m.IdEmpresa == entity.IdEmpresa).OrderByDescending(m => m.IdHistoricoEmpresa).ToList();

                Trazabilidad("Maestros", "Empresas", entity.Empresa, "Consulta", "Mantenimiento Empresas Histórico Modificaciones");
            }
        }

        protected void ModifyData(HistoricoEmpresas historico)
        {
            //var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Empresa Histórico").FirstOrDefault();
            //viewmodel = new FichaEmpresaHistoricoVM(baseVM, this.entity, historico);
            //baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
