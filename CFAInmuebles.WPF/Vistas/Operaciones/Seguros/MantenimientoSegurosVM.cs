using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CFAInmuebles.Domain.Models;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using DevExpress.Mvvm.Native;
using Microsoft.Extensions.Configuration;
using System.Windows;
using System.Threading;

namespace CFAInmuebles.WPF
{
    public class MantenimientoSegurosVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeSegurosVM baseVM;
        private HistoricoSeguros _selectedItem;
        private string _fechaseguro;


        public string Name
        {
            get
            {
                return "Mantenimiento Seguros";
            }
        }



        public HistoricoSeguros SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private Visibility visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;

                OnPropertyChanged("Visibility");
            }
        }

        private bool _isloading;
        public bool IsLoading
        {
            get
            {
                return _isloading;
            }
            set
            {
                _isloading = value;

                OnPropertyChanged("IsLoading");
            }
        }

        private string _mensajeresultado;
        public string MensajeResultado
        {
            get
            {
                return _mensajeresultado;
            }
            set
            {
                _mensajeresultado = value;

                OnPropertyChanged("MensajeResultado");
            }
        }

        public MantenimientoSegurosVM(HomeSegurosVM baseVM)
        {
            this.baseVM = baseVM;
        }

        private List<string> _fechasSeguro;
        public List<string> FechasSeguro
        {

            get { return _fechasSeguro; }
            set
            {
                if (_fechasSeguro != value)
                {
                    _fechasSeguro = value;
                    RaisePropertyChanged("FechasSeguro");
                }
            }
        }

        public string FechaSeguro
        {
            get { return _fechaseguro; }
            set
            {
                if (_fechaseguro != value)
                {
                    _fechaseguro = value;
                    RaisePropertyChanged("FechaSeguro");
                }
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).OrderBy(m => m.Inmueble).ToList();
            Inmuebles.Insert(0, new Inmuebles { Inmueble = "Seleccione:" });

            HistoricosSeguros = db.HistoricoSeguros.Where(m => m.FechaEliminacion == null && m.FechaSeguro != null).ToList();
            FechasSeguro = HistoricosSeguros.GroupBy(m => m.FechaSeguro.Value.Date).Select(m => m.Key.ToShortDateString()).ToList();
            FechasSeguro.Insert(0, "Seleccione:");

            SearchData();
        }

        protected override void SearchData()
        {
            base.SearchData();

            IsLoading = true;

            if (Inmueble != null && FechaSeguro != null)
                Trazabilidad("Operaciones", "Seguros", "", "Búsqueda", "Cadena de consulta: Inmueble=" + Inmueble.Inmueble + "&FechaSeguro=" + FechaSeguro);

            var search = db.HistoricoSeguros.Where(m => m.FechaEliminacion == null).AsQueryable();

            if (Inmueble != null && Inmueble.Inmueble != "Seleccione:")
            {
                search = search.Where(m => m.IdInmuebleNavigation.Inmueble == Inmueble.Inmueble);
            }

            if (FechaSeguro != null && FechaSeguro != "Seleccione:")
            {
                var endDate = Convert.ToDateTime(FechaSeguro).AddDays(1);
                search = search.Where(m => m.FechaSeguro >= Convert.ToDateTime(FechaSeguro) && m.FechaSeguro < endDate);
            }

            HistoricosSeguros = search.ToList();

            IsLoading = false;
        }

        protected override void ImportData()
        {
            base.ImportData();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Seguros").FirstOrDefault();
            viewmodel = new FichaSegurosVM(baseVM);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void GenerateFile()
        {
            base.GenerateFile();

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var FileName = configuration["PathsConfiguration:FileNameSeguros"] + "_" + DateTime.Now.ToShortDateString().Replace("/","") + ".xlsx";

            var newFile = new FileInfo(FileName);
            var Excel = new ExcelPackage(newFile);

            ExcelWorksheet newSheet;

            try
            {
                newSheet = Excel.Workbook.Worksheets.Add("Histórico seguros");
            }
            catch (Exception)
            {
                newSheet = Excel.Workbook.Worksheets["Histórico seguros"];
            }

            newSheet.Cells["A1"].Value = "Id. Inmueble";
            newSheet.Cells["B1"].Value = "Nombre Inmueble";
            newSheet.Cells["C1"].Value = "Continente";
            newSheet.Cells["D1"].Value = "Pérdida alquileres";
            newSheet.Cells["E1"].Value = "Total Daños";
            newSheet.Cells["F1"].Value = "Total Responsabilidad Civil";

            newSheet.Cells["A1:F1"].Style.Font.Bold = true;
            newSheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Double;
            newSheet.Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            newSheet.Cells["A1:F1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            newSheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            newSheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffb256"));

            int i = 0;      
            
            foreach(var inm in Inmuebles)
            {
                i++;
                if (i > 1)
                {
                    newSheet.Cells[i, 1].Value = inm.IdInmueble;
                    newSheet.Cells[i, 2].Value = inm.Inmueble;
                    newSheet.Cells[i, 1, i, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.DarkGray);
                }
            }

            newSheet.Protection.IsProtected = true;
            newSheet.Column(3).Style.Locked = false;
            newSheet.Column(4).Style.Locked = false;
            newSheet.Cells["E2:E2"].Style.Locked = false;
            newSheet.Cells["F2:F2"].Style.Locked = false;

            newSheet.Cells["E2:F2"].Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.DarkGray);

            const double minWidth = 20.00;
            const double maxWidth = 50.00;
            newSheet.Cells.AutoFitColumns(minWidth, maxWidth);

            Visibility = Visibility.Visible;


            try
            {
                Excel.Save();
                MensajeResultado = "Fichero generado correctamente.";
                Trazabilidad("Operaciones", "Seguros", FileName, "Generación de fichero", "Ficha Seguros");
            }
            catch(Exception ex)
            {

                MensajeResultado = "El fichero no ha podido generarse.";
            }

        }
    }
}
