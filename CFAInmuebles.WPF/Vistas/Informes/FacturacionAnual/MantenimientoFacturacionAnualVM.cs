using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoFacturacionAnualVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;
        protected ICommand _checkCommand;
        protected ICommand _uncheckCommand;

        private HomeFacturacionAnualVM baseVM;
        private Facturacion _selectedItem;
        private int? _año;
        private List<Inmuebles> InmueblesSelected;
        private Empresas _empresa;


        public  string Name
        {
            get
            {
                return "Mantenimiento Facturación Anual";
            }
        }

        public MantenimientoFacturacionAnualVM(HomeFacturacionAnualVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public int? Año
        {
            get { return _año; }
            set
            {
                _año = value;
                RaisePropertyChanged("Año");
            }
        }


        public Facturacion SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ExportData());
                }
                return _modifyCommand;
            }
        }

        public ICommand CheckCommand
        {
            get
            {
                if (_checkCommand == null)
                {
                    _checkCommand = new RelayCommand(p => CheckData((string)p));
                }
                return _checkCommand;
            }
        }

        public ICommand UnCheckCommand
        {
            get
            {
                if (_uncheckCommand == null)
                {
                    _uncheckCommand = new RelayCommand(p => UnCheckData((string)p));
                }
                return _uncheckCommand;
            }
        }

        public new Empresas Empresa
        {
            get { return _empresa; }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;

                    InmueblesSelected = new List<Inmuebles>();
                    Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdEmpresaNavigation.Empresa == _empresa.Empresa).ToList();
                    RaisePropertyChanged("Empresa");
                   
                }
            }
        }



        protected override void LoadData()
        {
            base.LoadData();

            InmueblesSelected = new List<Inmuebles>();

            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });

            ConceptosFacturacion = db.ConceptoFacturacion.Where(m => m.FechaEliminacion == null).ToList();
            ConceptosFacturacion.Insert(0, new ConceptoFacturacion { Conceptofacturacion1 = "Seleccione:" });

            CatalogarConceptos = db.CatalogarConcepto.ToList();
            CatalogarConceptos.Insert(0, new CatalogarConcepto { Valor = "Seleccione:" });

            SearchData();
            Trazabilidad("Informes", "Facturación Anual", "", "Consulta", "Mantenimiento Facturación Anual");           
        }

        protected void ExportData()
        {
            MemoryStream ms = new MemoryStream();

            using (ExcelPackage pack = new ExcelPackage(ms))
            {
                ExcelWorksheet hoja = pack.Workbook.Worksheets.Add("Facturación Anual");

                int columna = 1;
                int fila = 1;
                hoja = pack.Workbook.Worksheets[1];

                hoja.Cells["A1:AZ2000"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                hoja.Cells["A1:AZ2000"].Style.Fill.BackgroundColor.SetColor(Color.White);


                hoja.Cells["A1:AZ2000"].Style.Fill.BackgroundColor.SetColor(Color.White);

                hoja.Cells[fila, columna].Value = "Prueba";

                pack.SaveAs(ms);
                pack.Dispose();

                byte[] dt = ms.ToArray();

                string filePath = @"C:\Ficheros\ExcelDemo.xlsx";

                File.WriteAllBytes(filePath, dt);
           
            }
        }

        protected void CheckData(string entity)
        {
            var inmueble = db.Inmuebles.Where(m => m.Inmueble == entity).FirstOrDefault();
            if (inmueble != null)
                InmueblesSelected.Add(inmueble);
        }

        protected void UnCheckData(string entity)
        {
            var inmueble = db.Inmuebles.Where(m => m.Inmueble == entity).FirstOrDefault();
            if (inmueble != null)
                InmueblesSelected.Remove(inmueble);
        }

        protected override void SearchData()
        {
            base.SearchData();
            Trazabilidad("Informes", "Facturación Anual", "", "Búsqueda", "Cadena de consulta: Empresa=" + Empresa);

            if (Empresa != null)
            {
                var search = db.Facturacion.Where(m => m.FechaEliminacion == null).AsQueryable();

                search = search.Where(m => m.IdContratoClienteNavigation.IdEmpresaNavigation.Empresa == Empresa.Empresa);

                search = search.Where(m => InmueblesSelected.Contains(m.IdContratoClienteNavigation.IdInmuebleNavigation));

                if (Año != null)
                {
                    search = search.Where(m => m.Fecha != null && m.Fecha.Value.Year == Año);
                }

                if (ConceptoFacturacion != null)
                {
                    search = search.Where(m => m.IdConceptoFacturacionNavigation.Conceptofacturacion1 == ConceptoFacturacion.Conceptofacturacion1);
                }

                if (CatalogarConcepto != null)
                {
                    search = search.Where(m => m.IdConceptoFacturacionNavigation.IdCatalogarConceptoNavigation.Valor == CatalogarConcepto.Valor);
                }

                Facturas = search.ToList();
            }
           
        }
    }
}
