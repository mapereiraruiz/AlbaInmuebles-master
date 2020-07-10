using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaGastosVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Gastos entity;

        private HomeGastosVM baseVM;
        private string _gasto;
        private string _cuentacontable;
        private string _grupo;
        private bool? _em2;

        private bool _selectedItem;

        public AltaGastosVM(HomeGastosVM baseVM, Gastos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Gasto";
            }
        }

        public int MaxGasto
        {
            get
            {
                return 150;
            }
        }

        public int MaxCuentaContable
        {
            get
            {
                return 10;
            }
        }

        public int MaxGrupo
        {
            get
            {
                return 50;
            }
        }



        public new string Gasto
        {
            get {
                CheckValidationState("Gasto", _gasto); 
                return _gasto; }
            set
            {
                if (_gasto != value)
                {
                    CheckValidationState("Gasto", _gasto);
                    _gasto = value;
                    RaisePropertyChanged("Gasto");
                }
            }
        }

        public string CuentaContable
        {
            get {           
                return _cuentacontable; }
            set
            {
                if (_cuentacontable != value)
                {
                    _cuentacontable = value;
                    RaisePropertyChanged("CuentaContable");
                }
            }
        }

        public string Grupo
        {
            get
            {
                return _grupo;
            }
            set
            {
                if (_grupo != value)
                {
                    _grupo = value;
                    RaisePropertyChanged("Grupo");
                }
            }
        }

        public bool? Em2
        {
            get
            {
                return _em2;
            }
            set
            {
                if (_em2 != value)
                {
                    _em2 = value;
                    RaisePropertyChanged("Em2");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdTipoGasto == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        public bool SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        protected override void LoadData()
        {
            base.LoadData();
            TiposGasto = db.TipoGasto.ToList();
            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();

            if (entity.IdTipoGasto > 0)
            {
                Gasto = entity.Gasto;
                CuentaContable = entity.Cuentacontable;
                TipoGasto = entity.IdTipoGastoNavigation;
                Empresa = entity.IdEmpresaNavigation;
                Grupo = entity.Grupo;
                Em2 = entity.Em2;
                Trazabilidad("Mantenimientos", "Gastos", Gasto, "Consulta", "Ficha Gasto");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {

                var model = db.Gastos.Find(entity?.IdGasto);

                if (model == null)
                {
                    model = new Gastos();
                    db.Gastos.Add(model);
                    accion = "Alta";
                }

                model.Gasto = Gasto;
                model.Cuentacontable = CuentaContable;
                model.IdEmpresaNavigation = Empresa;
                model.IdTipoGastoNavigation = TipoGasto;
                model.Grupo = Grupo;
                model.Em2 = Em2;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Gastos", Gasto, accion, "Ficha Gasto");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Gasto").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Gasto").FirstOrDefault();
            viewmodel = new DeleteGastosVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
    
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Gasto").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Gasto")
            {
                var existe = db.Gastos.Where(m => m.Gasto == proposedValue as String).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Descripción es obligatorio.");
                    return false;
                }

                else if (existe != null && existe.IdGasto != entity.IdGasto)
                {
                    SetError(propertyName, "Ya existe una Descripción con ese valor.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            return true;
        }
    }
}
