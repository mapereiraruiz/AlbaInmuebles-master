using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoSwiftVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeSwiftVM baseVM;
        private Swift _selectedItem;
        private string _swift1;
        private string _banco;
      
        public string Name
        {
            get
            {
                return "Mantenimiento Swift";
            }
        }

        public MantenimientoSwiftVM(HomeSwiftVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string Swift1
        {
            get { return _swift1; }
            set
            {
                if (_swift1 != value)
                {
                    _swift1 = value;
                    RaisePropertyChanged("Swift");
                }
            }
        }

        public string Banco
        {
            get { return _banco; }
            set
            {
                if (_banco != value)
                {
                    _banco = value;
                    RaisePropertyChanged("Banco");
                }
            }
        }

        public Swift SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Swift)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();
            SearchData();
            Trazabilidad("Mantenimientos", "Swift", "", "Consulta", "Mantenimiento Swift");
        }

        protected void ModifyData(Swift entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Swift").FirstOrDefault();
            viewmodel = new FichaSwiftVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Mantenimientos", "Swift", "", "Búsqueda", "Cadena de consulta: Swift=" + Swift1 + "&Banco=" + Banco);

            var search = db.Swift.Where(m => m.FechaEliminacion == null).AsQueryable();
           
            if (!String.IsNullOrEmpty(Swift1))
                search = search.Where(m => m.Swift1.Contains(Swift1));

            if (!String.IsNullOrEmpty(Banco))
                search = search.Where(m => m.Banco.Contains(Banco));

            Swifts = search.ToList();
        }
    }
}
