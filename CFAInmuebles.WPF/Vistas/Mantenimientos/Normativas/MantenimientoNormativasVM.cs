using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoNormativasVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeNormativasVM baseVM;
        private Normas _selectedItem;
        private string _textodescriptivo;
        private string _archivodigital;

        public string Name
        {
            get
            {
                return "Mantenimiento Normativas";
            }
        }

        public MantenimientoNormativasVM(HomeNormativasVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string TextoDescriptivo
        {
            get { return _textodescriptivo; }
            set
            {
                if (_textodescriptivo != value)
                {
                    _textodescriptivo = value;
                    RaisePropertyChanged("TextoDescriptivo");
                }
            }
        }

        public Normas SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Normas)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoInstalaciones = db.TipoInstalacion.Where(m => m.FechaEliminacion == null).ToList();
            TipoInstalaciones.Insert(0, new TipoInstalacion { Valor = "Seleccione:" });

            Normas = db.Normas.Where(m => m.FechaEliminacion == null).ToList();
            SearchData();
            Trazabilidad("Mantenimientos", "Normativas", "", "Consulta", "Mantenimiento Normativas");
        }

        protected void ModifyData(Normas entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Normativas").FirstOrDefault();
            viewmodel = new FichaNormativasVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Mantenimientos", "Normativas", "", "Búsqueda", "Cadena de consulta: Texto Descriptivo=" + TextoDescriptivo + "&Tipo Instalación=" + TipoInstalacion?.Valor);

            var search = db.Normas.Where(m => m.FechaEliminacion == null).AsQueryable();

            if (!String.IsNullOrEmpty(TextoDescriptivo))
                search = search.Where(m => m.TextoDescriptivo.Contains(TextoDescriptivo));

            if (TipoInstalacion != null && TipoInstalacion.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoInstalacionNavigation == TipoInstalacion);
            }

            Normas = search.ToList();
        }
    }
}
