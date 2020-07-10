using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaSwiftVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Swift entity;

        private HomeSwiftVM baseVM;
        private string _swift1;
        private string _banco;
       
        private bool _selectedItem;

        public AltaSwiftVM(HomeSwiftVM baseVM, Swift entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Swift";
            }
        }

        public int MaxSwift
        {
            get
            {
                return 50;
            }
        }

        public int MaxBanco
        {
            get
            {
                return 200;
            }
        }

        public string Swift1
        {
            get {
                CheckValidationState("Swift1", _swift1); 
                return _swift1; }
            set
            {
                if (_swift1 != value)
                {
                    CheckValidationState("Swift1", _swift1);
                    _swift1 = value;
                    RaisePropertyChanged("Swift1");
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
        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdSwift == 0)
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
           
            if (entity.IdSwift > 0)
            {
                Swift1 = entity.Swift1;
                Banco = entity.Banco;
                Trazabilidad("Mantenimientos", "Swift", Swift1, "Consulta", "Ficha Swift");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.Swift.Find(entity?.IdSwift);

                if (model == null)
                {
                    model = new Swift();
                    db.Swift.Add(model);
                    accion = "Alta";
                }

                model.Swift1 = Swift1;
                model.Banco = Banco;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Swift", Swift1, accion, "Ficha Swift");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Swift").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            //Comprobamos que no tenga ningún contrato vinculado
            var contratos = db.ContratosClientes.Where(m => m.IdSwift == entity.IdSwift && m.FechaEliminacion == null).Any();

            if (!contratos)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Swift").FirstOrDefault();
                viewmodel = new DeleteSwiftVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = "Desvincule los contratos vinculados a este Swift para poder eliminarlo.";
            } 
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Swift").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Swift1")
            {
                var existe = db.Swift.Where(m => m.Swift1 == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Swift es obligatorio.");
                    return false;
                }

                else if (existe != null && existe.IdSwift != entity.IdSwift)
                {
                    SetError(propertyName, "Ya existe un Swift con ese valor.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Incremento")
            {

                if (proposedValue != null && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Incremento debe ser numérico.");
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