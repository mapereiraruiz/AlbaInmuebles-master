using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoTareaPeriodicaVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeTareaPeriodicaVM baseVM;
        private TareaPeriodica _selectedItem;
        private Tareas _tarea;
        private bool? _recurrente;
        
        public  string Name
        {
            get
            {
                return "Mantenimiento Tarea Periódica";
            }
        }

        public MantenimientoTareaPeriodicaVM(HomeTareaPeriodicaVM baseVM)
        {
            this.baseVM = baseVM;          
        }

        public bool? Recurrente
        {
            get { return _recurrente; }
            set
            {
                if (_recurrente != value)
                {
                    _recurrente = value;
                    RaisePropertyChanged("Recurrente");
                }
            }
        }


        public new Tareas Tarea
        {
            get { return _tarea; }
            set
            {
                if (_tarea != value)
                {
                    _tarea = value;
                    RaisePropertyChanged("Tarea");
                }
            }
        }

        public TareaPeriodica SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((TareaPeriodica)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.ToList();
            TipoFicheros.Insert(0, new TipoFichero { Valor = "Seleccione:" });
            
            Tareas = db.Tareas.Where(m => m.FechaEliminacion == null).ToList();
            Tareas.Insert(0, new Tareas { Tarea = "Seleccione:" });

            SearchData();
            Trazabilidad("Maestros", "Tareas Periódicas", "", "Consulta", "Mantenimiento Tarea Periódica");
        }

        protected void ModifyData(TareaPeriodica entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Tarea Periódica").FirstOrDefault();
            viewmodel  = new FichaTareaPeriodicaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Maestros", "Tareas Periódicas", "", "Búsqueda", "Cadena de consulta: Tarea=" + Tarea?.Tarea + "&Tipo Fichero=" + TipoFichero?.Valor
                + "&Recurrente=" + Recurrente);

            TareasPeriodicas = db.TareaPeriodica.Where(m => m.FechaEliminacion == null).ToList();

            //Comprobamos que tareas puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                TareasPeriodicas = TareasPeriodicas.Where(m => (inmuebles.Contains(m.IdFichero) && m.IdTipoFicheroNavigation.Valor == "Inmueble") || m.IdTipoFicheroNavigation.Valor != "Inmueble").ToList();
            }

            var listInmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdInmueble, i.Inmueble }).ToList();
            var listArticulos = db.Articulos.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdArticulo, i.Articulo }).ToList();
            var listContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoCliente, i.NombreCliente }).ToList();
            var listContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoProveedor, i.ReferenciaContrato }).ToList();
            var listEmpresas = db.Empresas.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdEmpresa, i.Empresa }).ToList();
            var listIncidencias = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdIncidencia, i.Incidencia }).ToList();
            var listObras = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdHistorialObra, i.Descripcion }).ToList();
            var listMantenimientos = db.Mantenimientos.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdMantenimiento, i.IdTipoMantenimientoNavigation.Valor }).ToList();

            foreach (var tarea in TareasPeriodicas)
            {
                switch (tarea.IdTipoFicheroNavigation.Valor)
                {
                    case "Inmueble":
                        tarea.TipoTareaDescripcion = listInmuebles.Where(m => m.IdInmueble == tarea.IdFichero).FirstOrDefault()?.Inmueble;
                        break; 
                    case "Artículo":
                        tarea.TipoTareaDescripcion = listArticulos.Where(m => m.IdArticulo == tarea.IdFichero).FirstOrDefault()?.Articulo;
                        break;
                    case "Contrato Cliente":
                        tarea.TipoTareaDescripcion = listContratosClientes.Where(m => m.IdContratoCliente == tarea.IdFichero).FirstOrDefault()?.NombreCliente;
                        break;
                    case "Contrato Proveedor":
                        tarea.TipoTareaDescripcion = listContratosProveedores.Where(m => m.IdContratoProveedor == tarea.IdFichero).FirstOrDefault()?.ReferenciaContrato;
                        break;                  
                    case "Empresa":
                        tarea.TipoTareaDescripcion = listEmpresas.Where(m => m.IdEmpresa == tarea.IdFichero).FirstOrDefault()?.Empresa;
                        break;
                    case "Incidencia":
                        tarea.TipoTareaDescripcion = listIncidencias.Where(m => m.IdIncidencia == tarea.IdFichero).FirstOrDefault()?.Incidencia;
                        break;
                    case "Obra":
                        tarea.TipoTareaDescripcion = listObras.Where(m => m.IdHistorialObra == tarea.IdFichero).FirstOrDefault()?.Descripcion;
                        break;
                    case "Mantenimiento":
                        tarea.TipoTareaDescripcion = listMantenimientos.Where(m => m.IdMantenimiento == tarea.IdFichero).FirstOrDefault()?.Valor;
                        break;
                }
            }


            var search = TareasPeriodicas.AsQueryable();

            //Comprobamos que tareas puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                search = search.Where(m => (inmuebles.Contains(m.IdFichero) && m.IdTipoFicheroNavigation.Valor == "Inmueble") || m.IdTipoFicheroNavigation.Valor != "Inmueble");
            }

            if (Tarea != null && Tarea.Tarea != "Seleccione:")
            {
                search = search.Where(m => m.IdTareaNavigation == Tarea);
            }


            if (TipoFichero != null && TipoFichero.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoFicheroNavigation == TipoFichero);
            }

            if (Recurrente != null)
            {
                search = search.Where(m => m.Recurrente == Recurrente);
            }

            TareasPeriodicas = search.ToList();
        }
    }
}
