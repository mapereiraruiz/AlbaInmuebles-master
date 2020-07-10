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
    public class AltaTipoLicenciaVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public TipoLicencia entity;
        private HomeTipoLicenciaVM baseVM;

        private int _idtipolicencia;
        private string _valor;

        private bool _selectedItem;

        public AltaTipoLicenciaVM(HomeTipoLicenciaVM baseVM, TipoLicencia entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                 return "Alta TipoLicencia";
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
                if (entity.IdTipoLicencia == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdTipoLicencia > 0)
            {
                Valor = entity.Valor;
                Trazabilidad("Mantenimientos", "Tipo Licencia", Valor, "Consulta", "Ficha Tipo Licencia");
            }
            else
            {
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.TipoLicencia.Find(entity?.IdTipoLicencia);

                if (model == null)
                {
                    model = new TipoLicencia();
                    db.TipoLicencia.Add(model);
                    accion = "Alta";
                }

                model.Valor= Valor;
                model.IdUsuarioNavigation = UserId;
                model.FechaSistema = DateTime.Now;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tipo Licencia", Valor, accion, "Ficha Tipo Licencia");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo de Licencia").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.TipoLicencia.Find(entity.IdTipoLicencia);

            var modelLicencias = db.Licencias.Where(m => m.IdTipoLicencia == entity.IdTipoLicencia).Any();

            if (!modelLicencias && entity!=null)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar TipoLicencia").FirstOrDefault();
                viewmodel = new DeleteTipoLicenciaVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = String.Empty;

                if (modelLicencias)
                    Mensaje = "Desvincule las Licencias a este Tipo de Licencia para poder eliminarlo. ";
            }
        }


        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo de Licencia").FirstOrDefault());
        }


        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Valor")
            {
                var existe = db.TipoLicencia.Where(m => m.Valor == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Valor es obligatorio.");
                    return false;
                }
                else if (existe != null && existe.IdTipoLicencia != entity.IdTipoLicencia)
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
