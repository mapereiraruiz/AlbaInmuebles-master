using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaNormativasVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Normas entity;

        private HomeNormativasVM baseVM;
        private string _textodescriptivo;
        private string _archivodigital;
        private bool _selectedItem;
        private DateTime? _fechavigencia;

        public AltaNormativasVM(HomeNormativasVM baseVM, Normas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Normativas";
            }
        }

        public int MaxTextoDescriptivo
        {
            get
            {
                return 250;
            }
        }

        public int MaxArchivoDigital
        {
            get
            {
                return 150;
            }
        }

        public string TextoDescriptivo
        {
            get {
                CheckValidationState("TextoDescriptivo", _textodescriptivo); 
                return _textodescriptivo; }
            set
            {
                if (_textodescriptivo != value)
                {
                    CheckValidationState("TextoDescriptivo", value);
                    _textodescriptivo = value;
                    RaisePropertyChanged("TextoDescriptivo");
                }
            }
        }

        public string ArchivoDigital
        {
            get { return _archivodigital; }
            set
            {
                if (_archivodigital != value)
                {
                    _archivodigital = value;
                    RaisePropertyChanged("ArchivoDigital");
                }
            }
        }

        public DateTime? FechaVigencia
        {
            get { return _fechavigencia; }
            set
            {
                if (_fechavigencia != value)
                {
                    _fechavigencia = value;
                    RaisePropertyChanged("FechaVigencia");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdNorma == 0)
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
            TipoInstalaciones = db.TipoInstalacion.Where(m => m.FechaEliminacion == null).ToList();
            TipoOrigenes = db.TipoOrigen.ToList();
           
            if (entity.IdNorma > 0)
            {
                TextoDescriptivo = entity.TextoDescriptivo;
                ArchivoDigital = entity.ArchivoDigital;
                FechaVigencia = entity.FechaVigencia;
                TipoInstalacion = entity.IdTipoInstalacionNavigation;
                TipoOrigen = entity.IdTipoOrigenNavigation;
                Trazabilidad("Mantenimientos", "Normativas", TextoDescriptivo, "Consulta", "Ficha Normativas");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.Normas.Find(entity?.IdNorma);

                if (model == null)
                {
                    model = new Normas();
                    db.Normas.Add(model);
                    accion = "Alta";
                }

                model.TextoDescriptivo = TextoDescriptivo;
                model.ArchivoDigital = ArchivoDigital;
                model.IdTipoInstalacionNavigation = TipoInstalacion;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;
                model.IdTipoOrigenNavigation = TipoOrigen;
                model.FechaVigencia = FechaVigencia;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Normativas", TextoDescriptivo, accion, "Ficha Normativas");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Normativas").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            //Comprobamos que no tenga ninguna entidad relacionada vinculada
            var mantenimientos = db.Mantenimientos.Where(m => m.IdNorma == entity.IdNorma && m.FechaEliminacion == null).Any();

            if (!mantenimientos)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Normativas").FirstOrDefault();
                viewmodel = new DeleteNormativasVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = "Desvincule los Mantenimientos vinculados a esta Normativa para poder eliminarla.";
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Normativas").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "TextoDescriptivo")
            {
                var existe = db.Normas.Where(m => m.TextoDescriptivo == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Texto Descriptivo es obligatorio.");
                    return false;
                }

                else if (existe != null && existe.IdNorma != entity.IdNorma)
                {
                    SetError(propertyName, "Ya existe una Normativa con ese valor.");
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
