using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class AltaTipoInstalacionVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public TipoInstalacion entity;

        private HomeTipoInstalacionVM baseVM;
        private string _valor;

        private bool _selectedItem;

        public AltaTipoInstalacionVM(HomeTipoInstalacionVM baseVM, TipoInstalacion entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                 return "Alta TipoInstalacion";
            }
        }

        public int MaxValor
        {
            get
            {
                return 150;
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

        public string Valor
        {
            get
            {
                CheckValidationState("Valor", _valor);
                return _valor;
            }
            set
            {
                if (_valor != value)
                {
                    CheckValidationState("Valor", value);
                    _valor = value;
                    RaisePropertyChanged("Valor");
                }
            }
        }


        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdTipoInstalacion == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();
           
            if (entity.IdTipoInstalacion > 0)
            {
                Valor = entity.Valor;
                SearchData();
                Trazabilidad("Mantenimientos", "Tipo Instalacion", Valor, "Consulta", "Ficha Tipo Instalacion");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.TipoInstalacion.Find(entity?.IdTipoInstalacion);

                if (model == null)
                { 
                    model = new TipoInstalacion();
                    db.TipoInstalacion.Add(model);
                    accion = "Alta";
                }

                model.Valor= Valor;
                model.IdUsuarioNavigation = UserId;
                model.FechaSistema = DateTime.Now;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tipo Instalacion", Valor, accion, "Ficha Tipo Instalacion");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo de Instalación").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar TipoInstalacion").FirstOrDefault();
                viewmodel = new DeleteTipoInstalacionVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
        }


        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo de Instalación").FirstOrDefault());
        }



        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Valor")
            {
                var existe = db.TipoInstalacion.Where(m => m.Valor == proposedValue as String).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Valor es obligatorio.");
                    return false;
                }
                else if (existe != null && existe.IdTipoInstalacion != entity.IdTipoInstalacion)
                {
                    SetError(propertyName, "Ya existe una descripción con ese valor.");
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
