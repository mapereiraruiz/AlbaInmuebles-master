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
    public class MantenimientoListadoAltasVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeListadoAltasVM baseVM;
        private ListadoAltas _selectedItem;
        private Empresas _empresa;
        private DateTime _fechadesde;
        private DateTime _fechahasta;

        public  string Name
        {
            get
            {
                return "Mantenimiento Listado Altas";
            }
        }

        public MantenimientoListadoAltasVM(HomeListadoAltasVM baseVM)
        {
            this.baseVM = baseVM;          
        }

        public new Empresas Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public DateTime FechaDesde
        {
            get { return _fechadesde; }
            set { _fechadesde = value;}
        }

        public DateTime FechaHasta
        {
            get { return _fechahasta; }
            set { _fechahasta = value; }
        }

        public ListadoAltas SelectedItem
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

            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });

            FechaDesde = DateTime.Now;
            FechaHasta = DateTime.Now;

            SearchData();
            Trazabilidad("Informes", "Inventario", "", "Consulta", "Listado Altas");
        }

                
        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Informes", "Inventario - Listado Altas", "", "Búsqueda", "Cadena de consulta: Empresa=" + Empresa +
                                                                            "&Fecha Desde=" + FechaDesde.ToString() + "&Fecha Hasta=" + FechaHasta.ToString());

            //ListadoAltas = db.Licencias.Where(m => m.FechaEliminacion == null).ToList();

            var resultado = (from HistorialOcupacion in db.HistorialOcupacion
                             join Articulos in db.Articulos on HistorialOcupacion.IdArticulo equals Articulos.IdArticulo
                             join Inmueble in db.Inmuebles on Articulos.IdInmueble equals Inmueble.IdInmueble
                             //join ContratoCliente in db.ContratosClientes on ContratoCliente.IdContratoCliente = HistorialOcupacion.IdContratoCliente

                             where Inmueble.IdEmpresa == _empresa.IdEmpresa 
                             select HistorialOcupacion
               );





            //var search = ListadoAltas.AsQueryable();




            //ListadoAltas = search.ToList();

        }
    }
}
