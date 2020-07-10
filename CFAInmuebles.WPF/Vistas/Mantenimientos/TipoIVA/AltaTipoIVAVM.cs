using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaTipoIVAVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public TipoIva entity;

        private HomeTipoIVAVM baseVM;
        private string _tipoiva1;
        private string _cuentacontable;
        private string _iva;

        private bool _selectedItem;

        public AltaTipoIVAVM(HomeTipoIVAVM baseVM, TipoIva entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Tipo IVA";
            }
        }

        public int MaxTipoIva
        {
            get
            {
                return 50;
            }
        }

        public int MaxCuentaContable
        {
            get
            {
                return 10;
            }
        }
        public string TipoIva1
        {
            get {
                CheckValidationState("TipoIva1", _tipoiva1); 
                return _tipoiva1; }
            set
            {
                if (_tipoiva1 != value)
                {
                    CheckValidationState("TipoIva1", _tipoiva1);
                    _tipoiva1 = value;
                    RaisePropertyChanged("TipoIva1");
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

        public string Iva
        {
            get {
                CheckValidationState("Iva", _iva); 
                return _iva; }
            set
            {
                if (_iva != value)
                {
                    CheckValidationState("Iva", _iva);
                    _iva = value;
                    RaisePropertyChanged("Iva");
                }
            }
        }
        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdTipoIva == 0)
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
            TipoIvas = db.TipoIva.Where(m => m.FechaEliminacion == null).ToList();
            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();

            if (entity.IdTipoIva > 0)
            {
                TipoIva1 = entity.TipoIva1;
                CuentaContable = entity.CuentaContable;
                Iva = entity.Iva?.ToString();
                Empresa = entity.IdEmpresaNavigation;
                Trazabilidad("Mantenimientos", "Tipos IVA", TipoIva1, "Consulta", "Ficha Tipo IVA");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {

                var model = db.TipoIva.Find(entity?.IdTipoIva);

                if (model == null)
                {
                    model = new TipoIva();
                    db.TipoIva.Add(model);
                    accion = "Alta";
                }

                model.TipoIva1 = TipoIva1;
                model.CuentaContable = CuentaContable;

                if (!String.IsNullOrEmpty(Iva))
                    model.Iva = int.Parse(Iva);

                model.IdEmpresaNavigation = Empresa;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tipos IVA", TipoIva1, accion, "Ficha Tipo IVA");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo IVA").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            //Comprobamos que no tenga ninguna entidad relacionada vinculada
            var conceptofacturacion = db.ConceptoFacturacion.Where(m => m.IdTipoIva == entity.IdTipoIva && m.FechaEliminacion == null).Any();

            if (!conceptofacturacion)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Tipo IVA").FirstOrDefault();
                viewmodel = new DeleteTipoIVAVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = "Desvincule los Conceptos de Facturación vinculados a este Tipo Iva para poder eliminarlo.";
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo IVA").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "TipoIva1")
            {
                var existe = db.TipoIva.Where(m => m.TipoIva1 == proposedValue as String).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Tipo Iva es obligatorio.");
                    return false;
                }

                else if (existe != null && existe.IdTipoIva != entity.IdTipoIva)
                {
                    SetError(propertyName, "Ya existe un Tipo Iva con ese valor.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Iva")
            {

                if (proposedValue != null && !int.TryParse(proposedValue as String, out int numValue))
                {
                    SetError(propertyName, "El campo Iva debe ser un entero.");
                    return false;
                }

                else if (proposedValue != null && double.TryParse(proposedValue as String, out double numPor) && numPor > 100)
                {
                    SetError(propertyName, "El campo Iva no puede ser mayor de 100.");
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
