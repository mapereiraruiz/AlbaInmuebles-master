using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows;


namespace CFAInmuebles.WPF
{
    public class AltaProvinciasVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Provincias entity;

        private HomeProvinciasVM baseVM;
        private string _provincia;
        private bool? _conveniofianza;
        private string _porcentajedeposito;
        private string _numeroconcierto;

        private bool _selectedItem;

        public AltaProvinciasVM(HomeProvinciasVM baseVM, Provincias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Provincias";
            }
        }

        public int MaxProvincia
        {
            get
            {
                return 100;
            }
        }

        public int MaxNumeroConcierto
        {
            get
            {
                return 10;
            }
        }


        public new string Provincia
        {
            get
            {
                CheckValidationState("Provincia", _provincia);
                return _provincia;
            }
            set
            {
                if (_provincia != value)
                {
                    CheckValidationState("Provincia", value);
                    _provincia = value;
                    RaisePropertyChanged("Provincia");
                }
            }
        }

        public bool? Conveniofianza
        {
            get { return _conveniofianza; }
            set
            {
                if (_conveniofianza != value)
                {
                    _conveniofianza = value;
                    RaisePropertyChanged("Conveniofianza");
                }
            }
        }

        public string PorcentajeDeposito
        {
            get { return _porcentajedeposito; }
            set
            {
                if (_porcentajedeposito != value)
                {
                    CheckValidationState("PorcentajeDeposito", value);
                    _porcentajedeposito = value;
                    RaisePropertyChanged("PorcentajeDeposito");
                }
            }
        }

        public string NumeroConcierto
        {
            get { return _numeroconcierto; }
            set
            {
                if (_numeroconcierto != value)
                {    
                    _numeroconcierto = value;
                    RaisePropertyChanged("NumeroConcierto");
                }
            }
        }


        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdProvincia == 0)
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
            Provincias = db.Provincias.Where(m => m.FechaEliminacion == null).ToList();

            if (entity.IdProvincia > 0)
            {
                Provincia = entity.Provincia;
                Conveniofianza = entity.Conveniofianza;
                NumeroConcierto = entity.NumeroConcierto;
                PorcentajeDeposito = entity.PorcentajeDeposito?.ToString();
                Trazabilidad("Mantenimientos", "Provincias", Provincia, "Consulta", "Ficha Provincia");
            }

            else
                Conveniofianza = true;
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.Provincias.Find(entity?.IdProvincia);

                if (model == null)
                {
                    model = new Provincias();
                    db.Provincias.Add(model);
                    accion = "Alta";
                }

                model.Provincia = Provincia;
                model.Conveniofianza = Conveniofianza;
                model.NumeroConcierto = NumeroConcierto;

                if (!string.IsNullOrEmpty(PorcentajeDeposito))
                    model.PorcentajeDeposito = double.Parse(PorcentajeDeposito.Replace('.', ','));

                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Provincias", Provincia, accion, "Ficha Provincia");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Provincias").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            //Comprobamos que no haya ninguna entidad relacionada vinculada

            var inmuebles = db.Inmuebles.Where(m => m.IdProvincia == entity.IdProvincia && m.FechaEliminacion == null).Any();
            
            if (!inmuebles)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Provincias").FirstOrDefault();
                viewmodel = new DeleteProvinciasVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = String.Empty;

                if (inmuebles)
                    Mensaje = "Desvincule los Inmuebles vinculados a esta Provincia para poder eliminarla." + Environment.NewLine;
               
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Provincias").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Provincia")
            {
                var existe = db.Provincias.Where(m => m.Provincia == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Provincia es obligatorio.");
                    return false;
                }

                else if (existe != null && existe.IdProvincia != entity.IdProvincia)
                {
                    SetError(propertyName, "Ya existe una Provincia con ese valor.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "PorcentajeDeposito")
            {

                if (proposedValue != null && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "El campo Porcentaje Depósito debe ser numérico.");
                    return false;
                }

                else if (proposedValue != null && double.TryParse(proposedValue as String, out double numPor) && numPor > 100)
                {
                    SetError(propertyName, "El campo Porcentaje Depósito no puede ser mayor de 100.");
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
