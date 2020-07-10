using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoPreventivoNormativoVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomePreventivoNormativoVM baseVM;
        private Mantenimientos _selectedItem;
        private string _descripcion;
        private TipoFichero _tipofichero;
        private List<String> _idficherovalues;
        private String _idficherovalue;

        public  string Name
        {
            get
            {
                return "Mantenimiento Preventivo y Normativo";
            }
        }

        public MantenimientoPreventivoNormativoVM(HomePreventivoNormativoVM baseVM)
        {
            this.baseVM = baseVM;          
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                if (_descripcion != value)
                {
                    _descripcion = value;
                    RaisePropertyChanged("Descripcion");
                }
            }
        }

        public String IdFicheroValue
        {
            get
            {
                return _idficherovalue;
            }
            set
            {
                if (_idficherovalue != value)
                {
                    _idficherovalue = value;

                    RaisePropertyChanged("IdFicheroValue");
                }
            }
        }

        public List<String> IdFicheroValues
        {

            get { return _idficherovalues; }
            set
            {
                if (_idficherovalues != value)
                {
                    _idficherovalues = value;
                    RaisePropertyChanged("IdFicheroValues");
                }
            }
        }

        public new TipoFichero TipoFichero
        {
            get
            {       
                return _tipofichero;
            }
            set
            {
                if (_tipofichero != value)
                {
                    _tipofichero = value;
                   
                    switch (_tipofichero?.Valor)
                    {
                        case "Inmueble":
                            IdFicheroValues = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.Inmueble).ToList();
                            break;
                        case "Artículo":
                            IdFicheroValues = db.Articulos.Where(m => m.FechaEliminacion == null).Select(m => m.Articulo).ToList();
                            break;
                     
                        case "Contrato Cliente":
                            IdFicheroValues = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(m => m.NombreCliente).ToList();
                            break;
                        case "Contrato Proveedor":
                            IdFicheroValues = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(m => m.ReferenciaContrato).ToList();
                            break;
                        case "Empresa":
                            IdFicheroValues = db.Empresas.Where(m => m.FechaEliminacion == null).Select(m => m.Empresa).ToList();
                            break;
                       
                        case "Obra":
                            IdFicheroValues = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(m => m.Descripcion).ToList();
                            break;
                        case "Incidencia":
                            IdFicheroValues = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(m => m.Incidencia).ToList();
                            break;
                    }

                    RaisePropertyChanged("TipoFichero");
                }
            }
        }

        public Mantenimientos SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Mantenimientos)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.Where(m => m.Valor == "Inmueble" || m.Valor == "Artículo").ToList();
            TipoFicheros.Insert(0, new TipoFichero { Valor = "Seleccione:" });

            TipoMantenimientos = db.TipoMantenimiento.ToList();
            TipoMantenimientos.Insert(0, new TipoMantenimiento { Valor = "Seleccione:" });

            ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).DistinctBy(m => m.NombreProveedor).ToList();
            ContratosProveedores.Insert(0, new ContratosProveedores { NombreProveedor = "Seleccione:" });

            TipoInstalaciones = db.TipoInstalacion.Where(m => m.FechaEliminacion == null).ToList();
            TipoInstalaciones.Insert(0, new TipoInstalacion { Valor = "Seleccione:" });

            SearchData();

            Trazabilidad("Mantenimientos", "Preventivo Normativo", "", "Consulta", "Mantenimiento Preventivo Normativo");

        }

        protected void ModifyData(Mantenimientos entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha PreventivoNormativo").FirstOrDefault();
            viewmodel  = new FichaPreventivoNormativoVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Preventivo Normativo", "", "Búsqueda", "Cadena de consulta: Fichero=" + IdFicheroValue +
                                                                            "&Tipo Fichero=" + TipoFichero?.Valor +
                                                                            "&Tipo Mantenimiento=" + TipoMantenimiento?.Valor +
                                                                            "&Proveedor=" + ContratoProveedor?.NombreProveedor +
                                                                            "&Tipo Instalacion=" + TipoInstalacion?.Valor);

            Mantenimientos = db.Mantenimientos.Where(m => m.FechaEliminacion == null).ToList();

            //Comprobamos que inmuebles puede ver el usuario
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                Mantenimientos = Mantenimientos.Where(m => (inmuebles.Contains(m.IdFichero) && m.IdTipoFicheroNavigation.Valor == "Inmueble") || m.IdTipoFicheroNavigation.Valor != "Inmueble").ToList();
            }

            var listInmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdInmueble, i.Inmueble }).ToList();
            var listArticulos = db.Articulos.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdArticulo, i.Articulo }).ToList();
            var listContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoCliente, i.NombreCliente }).ToList();
            var listContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoProveedor, i.NombreProveedor }).ToList();
            var listEmpresas = db.Empresas.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdEmpresa, i.Empresa }).ToList();
            var listIncidencias = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdIncidencia, i.Incidencia }).ToList();
            var listObras = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdHistorialObra, i.Descripcion }).ToList();

            foreach (var mantenimiento in Mantenimientos)
            {
                switch (mantenimiento.IdTipoFicheroNavigation.Valor)
                {
                    case "Inmueble":
                        mantenimiento.TipoMantenimientoDescripcion = listInmuebles.Where(m => m.IdInmueble == mantenimiento.IdFichero).FirstOrDefault()?.Inmueble;
                        break;            
                    case "Artículo":
                        mantenimiento.TipoMantenimientoDescripcion = listArticulos.Where(m => m.IdArticulo == mantenimiento.IdFichero).FirstOrDefault()?.Articulo;
                        break;
                    case "Contrato Cliente":
                        mantenimiento.TipoMantenimientoDescripcion = listContratosClientes.Where(m => m.IdContratoCliente == mantenimiento.IdFichero).FirstOrDefault()?.NombreCliente;
                        break;
                    case "Contrato Proveedor":
                        mantenimiento.TipoMantenimientoDescripcion = listContratosProveedores.Where(m => m.IdContratoProveedor == mantenimiento.IdFichero).FirstOrDefault()?.NombreProveedor;
                        break; 
                    case "Empresa":
                        mantenimiento.TipoMantenimientoDescripcion = listEmpresas.Where(m => m.IdEmpresa == mantenimiento.IdFichero).FirstOrDefault()?.Empresa;
                        break;
                    case "Incidencia":
                        mantenimiento.TipoMantenimientoDescripcion = listIncidencias.Where(m => m.IdIncidencia == mantenimiento.IdFichero).FirstOrDefault()?.Incidencia;
                        break;
                    case "Obra":
                        mantenimiento.TipoMantenimientoDescripcion = listObras.Where(m => m.IdHistorialObra == mantenimiento.IdFichero).FirstOrDefault()?.Descripcion;
                        break;

                }
            }


            var search = Mantenimientos.AsQueryable();

            if (IdFicheroValue != null && IdFicheroValue != "Seleccione:")
            {
                search = search.Where(m => m.TipoMantenimientoDescripcion.Contains(IdFicheroValue));
            }

            if (TipoFichero != null && TipoFichero.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoFicheroNavigation == TipoFichero);
            }

            if (TipoMantenimiento != null && TipoMantenimiento.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoMantenimientoNavigation == TipoMantenimiento);
            }

            if (ContratoProveedor != null && ContratoProveedor.NombreProveedor != "Seleccione:")
            {
                search = search.Where(m => m.IdContratoProveedorNavigation.NombreProveedor == ContratoProveedor.NombreProveedor);
            }

            if (TipoInstalacion != null && TipoInstalacion.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoInstalacionNavigation == TipoInstalacion);
            }

            Mantenimientos = search.ToList();
        }
    }
}
