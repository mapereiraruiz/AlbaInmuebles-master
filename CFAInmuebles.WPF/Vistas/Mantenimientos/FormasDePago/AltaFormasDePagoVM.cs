using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaFormasDePagoVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        
        public FormasDePago entity;

        private HomeFormasDePagoVM baseVM;
        private string _formapago;
        private string _codigoformapago;
        private bool _selectedItem;

        public AltaFormasDePagoVM(HomeFormasDePagoVM baseVM, FormasDePago entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Formas De Pago";
            }
        }

        public int MaxFormaPago
        {
            get
            {
                return 150;
            }
        }
        public string FormaPago
        {
            get {
                CheckValidationState("FormaPago", _formapago); 
                return _formapago; }
            set
            {
                if (_formapago != value)
                {
                    CheckValidationState("FormaPago", _formapago);
                    _formapago = value;
                    RaisePropertyChanged("FormaPago");
                }
            }
        }

        public string CodigoFormaPago
        {
            get {
                CheckValidationState("CodigoFormaPago", _codigoformapago); 
                return _codigoformapago; }
            set
            {
                if (_codigoformapago != value)
                {
                    CheckValidationState("CodigoFormaPago", _codigoformapago);
                    _codigoformapago = value;
                    RaisePropertyChanged("CodigoFormaPago");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdFormaPago == 0)
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

            if (entity.IdFormaPago > 0)
            {
                FormaPago = entity.FormaPago;
                CodigoFormaPago = entity.CodigoFormaPago.ToString();
                Trazabilidad("Mantenimientos", "Formas de Pago", FormaPago, "Consulta", "Ficha Formas de Pago");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.FormasDePago.Find(entity?.IdFormaPago);

                if (model == null)
                {
                    model = new FormasDePago();
                    db.FormasDePago.Add(model);
                    accion = "Alta";
                }

                model.FormaPago = FormaPago;
                model.CodigoFormaPago = int.Parse(CodigoFormaPago);
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Formas de Pago", FormaPago, accion, "Ficha Concepto Facturación");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Formas De Pago").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            //Comprobamos que no tenga ninguna entidad relacionada vinculada
            var contratos = db.ContratosClientes.Where(m => m.IdFormaPago == entity.IdFormaPago && m.FechaEliminacion == null).Any();

            if (!contratos)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Formas de Pago").FirstOrDefault();
                viewmodel = new DeleteFormasDePagoVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = "Desvincule los contratos vinculados a esta Forma de Pago para poder eliminarla.";
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Formas De Pago").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "FormaPago")
            {
                var existe = db.FormasDePago.Where(m => m.FormaPago == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Forma Pago es obligatorio.");
                    return false;
                }

                else if (existe != null && existe.IdFormaPago != entity.IdFormaPago)
                {
                    SetError(propertyName, "Ya existe una Forma Pago con ese valor.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "CodigoFormaPago")
            {
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Código Forma Pago es obligatorio.");
                    return false;
                }

                else if (proposedValue != null && !int.TryParse(proposedValue as String, out int numValue))
                {
                    SetError(propertyName, "El campo Código Forma Pago debe ser un entero.");
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

