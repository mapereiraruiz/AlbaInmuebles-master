using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaTareaVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Tareas entity;

        private HomeTareaVM baseVM;

        private string _tarea;
        private TipoFichero _tipofichero;
        private string _descripcion;

        private bool _selectedItem;

        public AltaTareaVM(HomeTareaVM baseVM, Tareas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                 return "Alta Tarea";
            }
        }

        public int MaxTarea
        {
            get
            {
                return 100;
            }
        }

        public int MaxDescripcion
        {
            get
            {
                return 150;
            }
        }

        public new string Tarea
        {
            get
            {
                CheckValidationState("Tarea", _tarea);
                return _tarea;
            }
            set
            {
                if (_tarea != value)
                {
                    CheckValidationState("Tarea", value);
                    _tarea = value;
                    RaisePropertyChanged("Tarea");
                }
            }
        }

        public new TipoFichero TipoFichero
        {
            get
            {
                CheckValidationState("TipoFichero", _tipofichero);
                return _tipofichero;
            }
            set
            {
                if (_tipofichero != value)
                {
                    CheckValidationState("TipoFichero", _tipofichero);
                    _tipofichero = value;
                    entity.IdTipoFicheroNavigation = _tipofichero;
                    RaisePropertyChanged("TipoFichero");
                }
            }
        }


        public string Descripcion
        {
            get
            {
                CheckValidationState("Descripcion", _descripcion);
                return _descripcion;
            }
            set
            {
                if (_descripcion != value)
                {
                    CheckValidationState("Descripcion", value);
                    _descripcion = value;
                    RaisePropertyChanged("Descripcion");
                }
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

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdTarea == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.ToList();

            if (entity.IdTarea > 0)
            {
                TipoFichero = entity.IdTipoFicheroNavigation;
                Descripcion = entity.Descripcion;
                Tarea = entity.Tarea;
                Trazabilidad("Mantenimientos", "Tareas", Tarea, "Consulta", "Ficha Tareas");
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
                var model = db.Tareas.Find(entity?.IdTarea);

                if (model == null)
                {
                    model = new Tareas();
                    db.Tareas.Add(model);
                    accion = "Alta";
                }
 
                model.IdTipoFicheroNavigation = TipoFichero;
                model.Descripcion = Descripcion;
                model.Tarea = Tarea;
                model.IdUsuarioNavigation = UserId;
                model.FechaSistema = DateTime.Now;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tareas", Tarea, accion, "Ficha Tareas");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tarea").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Tarea").FirstOrDefault();
                viewmodel = new DeleteTareaVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tarea").FirstOrDefault());
        }


        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Tarea")
            {
                var existe = db.Tareas.Where(m => m.Tarea == proposedValue as String).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Tarea es obligatorio.");
                    return false;
                }
                else if (existe != null && existe.IdTarea != entity.IdTarea)
                {
                    SetError(propertyName, "Ya existe una tarea con ese valor.");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Descripcion")
            {
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Descripción es obligatorio.");
                    return false;
                }
               
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }


            if (propertyName == "TipoFichero")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Tipo Fichero es obligatorio.");
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
