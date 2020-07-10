using CFAInmuebles.Domain.Models;
using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoIncidenciasVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeIncidenciasVM baseVM;
        private Incidencias _selectedItem;
        private string _incidencia;
        private DateTime? _fechaincidencia;

        public string Name
        {
            get
            {
                return "Mantenimiento Incidencias";
            }
        }

        public MantenimientoIncidenciasVM(HomeIncidenciasVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string Incidencia
        {
            get { return _incidencia; }
            set
            {
                if (_incidencia != value)
                {
                    _incidencia = value;
                    RaisePropertyChanged("Incidencia");
                }
            }
        }

        public DateTime? FechaIncidencia
        {
            get { return _fechaincidencia; }
            set
            {
                if (_fechaincidencia != value)
                {
                    _fechaincidencia = value;
                    RaisePropertyChanged("FechaIncidencia");
                }
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

        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.ToList();
            TipoFicheros.Insert(0, new TipoFichero { Valor = "Seleccione:" });
            SearchData();
            Trazabilidad("Maestros", "Incidencias", "", "Consulta", "Mantenimiento Incidencias");
        }

        protected void ModifyData(Incidencias entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Incidencias").FirstOrDefault();
            viewmodel = new FichaIncidenciasVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Maestros", "Incidencias", "", "Búsqueda", "Cadena de consulta: Incidencia=" + Incidencia +
                                                                                       "&Tipo Fichero=" + TipoFichero?.Valor +
                                                                                       "&Fecha Incidencia=" + FechaIncidencia);

            
            Incidencias = db.Incidencias.Where(m => m.FechaEliminacion == null).ToList();

            //Comprobamos que inmuebles puede ver el usuario

            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                Incidencias = Incidencias.Where(m => (inmuebles.Contains(m.IdFichero) && m.IdTipoFicheroNavigation.Valor == "Inmueble") || m.IdTipoFicheroNavigation.Valor != "Inmueble").ToList();
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


            var search = Incidencias.AsQueryable();

            if (!String.IsNullOrEmpty(Incidencia))
                search = search.Where(m => m.Incidencia.Contains(Incidencia));

            if (TipoFichero != null && TipoFichero.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoFicheroNavigation == TipoFichero);
            }

            if (FechaIncidencia != null)
            {
                var endDate = FechaIncidencia?.AddDays(1);
                search = search.Where(m => m.FechaIncidencia >= FechaIncidencia && m.FechaIncidencia < endDate);
            }
            Incidencias = search.ToList();
        }
    }
}
