using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using DevExpress.Data.ODataLinq.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoLicenciaVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeLicenciaVM baseVM;
        private Licencias _selectedItem;
        private string _obra;

        public  string Name
        {
            get
            {
                return "Mantenimiento Licencia";
            }
        }

        public MantenimientoLicenciaVM(HomeLicenciaVM baseVM)
        {
            this.baseVM = baseVM;          
        }

        public String Obra
        {
            get
            {   
                return _obra;
            }
            set
            {
                if (_obra != value)
                {
                    _obra = value;
                    RaisePropertyChanged("Obra");
                }
            }
        }

        public Licencias SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Licencias)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.ToList();
            TipoFicheros.Insert(0, new TipoFichero { Valor = "Seleccione:" });
            TiposLicencia = db.TipoLicencia.Where(m => m.FechaEliminacion == null).ToList();
            TiposLicencia.Insert(0, new TipoLicencia { Valor = "Seleccione:" });

            SearchData();
            Trazabilidad("Mantenimientos", "Licencias", "", "Consulta", "Mantenimiento Licencias");
        }

        protected void ModifyData(Licencias entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Licencia").FirstOrDefault();
            viewmodel  = new FichaLicenciaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Licencias", "", "Búsqueda", "Cadena de consulta: Tipo Fichero=" + TipoFichero?.Valor +
                                                                            "&Tipo Licencia=" + TipoLicencia?.Valor + "&Obra=" + Obra);

            Licencias = db.Licencias.Where(m => m.FechaEliminacion == null).ToList();

            //Comprobamos que inmuebles puede ver el usuario
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                Licencias = Licencias.Where(m => (inmuebles.Contains(m.IdFichero) && m.IdTipoFicheroNavigation.Valor == "Inmueble") || m.IdTipoFicheroNavigation.Valor != "Inmueble").ToList();
            }

            var listInmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdInmueble, i.Inmueble }).ToList();
            var listArticulos = db.Articulos.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdArticulo, i.Articulo }).ToList();
            var listContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoCliente, i.NombreCliente }).ToList();
            var listContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoProveedor, i.NombreProveedor }).ToList();
            var listEmpresas = db.Empresas.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdEmpresa, i.Empresa }).ToList();
            var listIncidencias = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdIncidencia, i.Incidencia }).ToList();
            var listObras = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdHistorialObra, i.Descripcion }).ToList();

            foreach (var licencia in Licencias)
            {
                switch (licencia.IdTipoFicheroNavigation.Valor)
                {
                    case "Inmueble":
                        licencia.TipoLicenciaDescripcion = listInmuebles.Where(m => m.IdInmueble == licencia.IdFichero).FirstOrDefault()?.Inmueble;
                        break;                   
                    case "Artículo":
                        licencia.TipoLicenciaDescripcion = listArticulos.Where(m => m.IdArticulo == licencia.IdFichero).FirstOrDefault()?.Articulo;
                        break;
                    case "Contrato Cliente":
                        licencia.TipoLicenciaDescripcion = listContratosClientes.Where(m => m.IdContratoCliente == licencia.IdFichero).FirstOrDefault()?.NombreCliente;
                        break;
                    case "Contrato Proveedor":
                        licencia.TipoLicenciaDescripcion = listContratosProveedores.Where(m => m.IdContratoProveedor == licencia.IdFichero).FirstOrDefault()?.NombreProveedor;
                        break;
                  
                    case "Empresa":
                        licencia.TipoLicenciaDescripcion = listEmpresas.Where(m => m.IdEmpresa == licencia.IdFichero).FirstOrDefault()?.Empresa;
                        break;
                    case "Incidencia":
                        licencia.TipoLicenciaDescripcion = listIncidencias.Where(m => m.IdIncidencia == licencia.IdFichero).FirstOrDefault()?.Incidencia;
                        break;
                    case "Obra":
                        licencia.TipoLicenciaDescripcion = listObras.Where(m => m.IdHistorialObra == licencia.IdFichero).FirstOrDefault()?.Descripcion;
                        break;

                }
            }


            var search = Licencias.AsQueryable();

            //Comprobamos que inmuebles puede ver el usuario
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                search = search.Where(m => (inmuebles.Contains(m.IdFichero) && m.IdTipoFicheroNavigation.Valor == "Inmueble") || m.IdTipoFicheroNavigation.Valor != "Inmueble");
            }

            if (TipoFichero != null && TipoFichero.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoFicheroNavigation == TipoFichero);
            }

            if (TipoLicencia != null && TipoLicencia.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoLicenciaNavigation == TipoLicencia);
            }

            if (!String.IsNullOrEmpty(Obra))
                search = search.Where(m => m.TipoLicenciaDescripcion.Contains(Obra));


            Licencias = search.ToList();

        }
    }
}
