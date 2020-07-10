using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class IncidenciasyTareasVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;
        protected ICommand _modifyCommandTareas;

        private Incidencias _selectedItem;
        private TareaPeriodica _selectedItemTarea;
        public IncidenciasyTareasVM()
        {

            //Comprobamos que inmuebles puede ver el usuario

            if (UserId != null)
            {
                Incidencias = db.Incidencias.Where(m => m.FechaEliminacion == null).ToList();
                TareasPeriodicas = db.TareaPeriodica.Where(m => m.FechaEliminacion == null).ToList();

                if (!UserId.Administrador)
                {
                    var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    Incidencias = Incidencias.Where(m => (inmuebles.Contains(m.IdFichero) && m.IdTipoFicheroNavigation.Valor == "Inmueble") || m.IdTipoFicheroNavigation.Valor != "Inmueble").ToList();
                    TareasPeriodicas = TareasPeriodicas.Where(m => (inmuebles.Contains(m.IdFichero) && m.IdTipoFicheroNavigation.Valor == "Inmueble") || m.IdTipoFicheroNavigation.Valor != "Inmueble").ToList();
                    TareasPeriodicas = TareasPeriodicas.Where(m => m.AsignadaA == UserId.IdUsuario).ToList();
                }

                var listInmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdInmueble, i.Inmueble }).ToList();
                var listArticulos = db.Articulos.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdArticulo, i.Articulo }).ToList();
                var listContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoCliente, i.NombreCliente }).ToList();
                var listContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoProveedor, i.NombreProveedor }).ToList();
                var listEmpresas = db.Empresas.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdEmpresa, i.Empresa }).ToList();
                var listIncidencias = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdIncidencia, i.Incidencia }).ToList();
                var listObras = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdHistorialObra, i.Descripcion }).ToList();
                var listMantenimientos = db.Mantenimientos.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdMantenimiento, i.IdTipoMantenimientoNavigation.Valor }).ToList();

                foreach (var incidencia in Incidencias)
                {
                    switch (incidencia.IdTipoFicheroNavigation.Valor)
                    {
                        case "Inmueble":
                            incidencia.TipoIncidenciaDescripcion = listInmuebles.Where(m => m.IdInmueble == incidencia.IdFichero).FirstOrDefault()?.Inmueble;
                            break;         
                        case "Artículo":
                            incidencia.TipoIncidenciaDescripcion = listArticulos.Where(m => m.IdArticulo == incidencia.IdFichero).FirstOrDefault()?.Articulo;
                            break;
                        case "Contrato Cliente":
                            incidencia.TipoIncidenciaDescripcion = listContratosClientes.Where(m => m.IdContratoCliente == incidencia.IdFichero).FirstOrDefault()?.NombreCliente;
                            break;
                        case "Contrato Proveedor":
                            incidencia.TipoIncidenciaDescripcion = listContratosProveedores.Where(m => m.IdContratoProveedor == incidencia.IdFichero).FirstOrDefault()?.NombreProveedor;
                            break; 
                        case "Empresa":
                            incidencia.TipoIncidenciaDescripcion = listEmpresas.Where(m => m.IdEmpresa == incidencia.IdFichero).FirstOrDefault()?.Empresa;
                            break;
                        case "Incidencia":
                            incidencia.TipoIncidenciaDescripcion = listIncidencias.Where(m => m.IdIncidencia == incidencia.IdFichero).FirstOrDefault()?.Incidencia;
                            break;
                        case "Obra":
                            incidencia.TipoIncidenciaDescripcion = listObras.Where(m => m.IdHistorialObra == incidencia.IdFichero).FirstOrDefault()?.Descripcion;
                            break;
                        case "Mantenimiento":
                            incidencia.TipoIncidenciaDescripcion = listMantenimientos.Where(m => m.IdMantenimiento == incidencia.IdFichero).FirstOrDefault()?.Valor;
                            break;
                    }
                }
           
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
                            tarea.TipoTareaDescripcion = listContratosProveedores.Where(m => m.IdContratoProveedor == tarea.IdFichero).FirstOrDefault()?.NombreProveedor;
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
            }
        }

        public string Name
        {
            get
            {
                return "Incidencias y Tareas";
            }
        }

        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData((Incidencias)p));
                }
                return _modifyCommand;
            }
        }

        public ICommand ModifyCommandTareas
        {
            get
            {
                if (_modifyCommandTareas == null)
                {
                    _modifyCommandTareas = new RelayCommand(p => ModifyDataTareas((TareaPeriodica)p));
                }
                return _modifyCommandTareas;
            }
        }

        public Incidencias SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public TareaPeriodica SelectedItemTarea
        {
            get { return _selectedItemTarea; }
            set
            {
                _selectedItemTarea = value;
                RaisePropertyChanged("SelectedItemTarea");
            }
        }

        protected void ModifyData(Incidencias incidencia)
        {
            HomeIncidencias ventana = new HomeIncidencias();

            HomeIncidenciasVM datacontext = new HomeIncidenciasVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaIncidenciasVM(datacontext, incidencia);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }

        protected void ModifyDataTareas(TareaPeriodica tarea)
        {
            HomeTareaPeriodica ventana = new HomeTareaPeriodica();

            HomeTareaPeriodicaVM datacontext = new HomeTareaPeriodicaVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaTareaPeriodicaVM(datacontext, tarea);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();

        }
    }
}