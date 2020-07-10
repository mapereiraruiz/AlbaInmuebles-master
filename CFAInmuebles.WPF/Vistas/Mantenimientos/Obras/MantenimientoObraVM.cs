using CFAInmuebles.Domain.Models;
using DevExpress.Data.ODataLinq.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoObraVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeObraVM baseVM;
        private HistorialObra _selectedItem;
        private List<String> _idficherovalues;
        private String _idficherovalue;
        private TipoFichero _tipofichero;

        public  string Name
        {
            get
            {
                return "Mantenimiento Obra";
            }
        }

        public MantenimientoObraVM(HomeObraVM baseVM)
        {
            this.baseVM = baseVM;          
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

                    if (_idficherovalue != null && _idficherovalue != "Seleccione:")
                    {
                        switch (_tipofichero?.Valor)
                        {
                            case "Inmueble":
                                _idficherovalue = db.Inmuebles.Where(m => m.Inmueble == _idficherovalue).FirstOrDefault().IdInmueble.ToString();
                                break;
                            case "Artículo":
                                _idficherovalue = db.Articulos.Where(m => m.Articulo == _idficherovalue).FirstOrDefault().IdArticulo.ToString();
                                break;
                            case "Empresa":
                                _idficherovalue = db.Empresas.Where(m => m.Empresa == _idficherovalue).FirstOrDefault().IdEmpresa.ToString();
                                break;
                         
                        }     
                    }

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
                        case "Empresa":
                            IdFicheroValues = db.Empresas.Where(m => m.FechaEliminacion == null).Select(m => m.Empresa).ToList();
                            break;
                        
                    }

                    IdFicheroValues.Insert(0, "Seleccione:");
                    RaisePropertyChanged("TipoFichero");
                    RaisePropertyChanged("IdFicheroValues");
                }
            }
        }

        public HistorialObra SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistorialObra)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.Where( m=> m.Valor == "Artículo" || m.Valor == "Inmueble" || m.Valor == "Empresa" || m.Valor == "Cliente").ToList();
            TipoFicheros.Insert(0, new TipoFichero { Valor = "Seleccione:" });

            TipoObras = db.TipoObra.ToList();
            TipoObras.Insert(0, new TipoObra { Valor = "Seleccione:" });

            SearchData();

            Trazabilidad("Mantenimientos", "Obras", "", "Consulta", "Mantenimiento Obras");
        }

        protected void ModifyData(HistorialObra entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Obra").FirstOrDefault();
            viewmodel = new FichaObraVM(baseVM,  entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
        
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Mantenimientos", "Obras", "", "Búsqueda", "Cadena de consulta: Tipo Fichero=" + TipoFichero?.Valor +
                                                                            "&Tipo Obra=" + TipoObra?.Valor +
                                                                            "& IdFichero=" + IdFicheroValue);

            Obras = db.HistorialObra.Where(m => m.FechaEliminacion == null).ToList();

            var listInmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdInmueble, i.Inmueble }).ToList();
            var listArticulos = db.Articulos.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdArticulo, i.Articulo }).ToList();
            var listEmpresas = db.Empresas.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdEmpresa, i.Empresa }).ToList();
            var listContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoCliente, i.NombreCliente }).ToList();

            foreach (var obra in Obras)
            {
                switch (obra.IdTipoFicheroNavigation.Valor)
                {
                    case "Inmueble":
                        obra.TipoObraDescripcion = listInmuebles.Where(m => m.IdInmueble == obra.IdFichero).FirstOrDefault()?.Inmueble;
                        break;
                    case "Artículo":
                        obra.TipoObraDescripcion = listArticulos.Where(m => m.IdArticulo == obra.IdFichero).FirstOrDefault()?.Articulo;
                        break;
                    case "Empresa":
                        obra.TipoObraDescripcion = listEmpresas.Where(m => m.IdEmpresa == obra.IdFichero).FirstOrDefault()?.Empresa;
                        break;
                    case "Contrato Cliente":
                        obra.TipoObraDescripcion = listContratosClientes.Where(m => m.IdContratoCliente == obra.IdFichero).FirstOrDefault()?.NombreCliente;
                        break;
                }
            }

            var search = Obras.AsQueryable();

            if (TipoFichero != null && TipoFichero.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoFicheroNavigation == TipoFichero);
            }

            if (TipoObra != null && TipoObra.Valor != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoObraNavigation == TipoObra);
            }

            if (IdFicheroValue != null)
            {
                search = search.Where(m => m.IdFichero == int.Parse(IdFicheroValue));
                IdFicheroValues.Insert(0, "Seleccione:");
                RaisePropertyChanged("IdFicheroValues");
                RaisePropertyChanged("IdFicheroValue");
            }

            Obras = search.ToList();
        }
    }
}
