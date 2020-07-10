using CFAInmuebles.Domain.Models;
using DevExpress.DataAccess.Native.Data;
using DevExpress.Xpf.Editors;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands.Internal;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ImportarSegurosVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public HistoricoSeguros entity;
        private HomeSegurosVM baseVM;

        private HistoricoSeguros _selectedItem;
        private DateTime? _fechaseguro;
        private DateTime? _fechaincorporacion;

        public ImportarSegurosVM(HomeSegurosVM baseVM, HistoricoSeguros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Importar Seguros";
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

        public DateTime? FechaSeguro
        {
            get { return _fechaseguro; }
            set
            {
                if (_fechaseguro != value)
                {
                    _fechaseguro = value;

                    FechaSeguro = _fechaseguro;
                    RaisePropertyChanged("FechaSeguro");
                }
            }
        }

        public DateTime? FechaIncorporacion
        {
            get { return _fechaincorporacion; }
            set
            {
                if (_fechaincorporacion != value)
                {
                    _fechaincorporacion = value;

                    FechaIncorporacion = _fechaincorporacion;
                    RaisePropertyChanged("FechaIncorporacion");
                }
            }
        }

        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => LoadFile((HistoricoSeguros)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            FechaSeguro = DateTime.Now;
            FechaIncorporacion = DateTime.Now;
        }

        protected void LoadFile(HistoricoSeguros entity)
        {
            if (entity != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Worksheets|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var model = db.HistoricoSeguros.Where(m => m.IdInmueble == entity.IdInmueble
                                    && m.IdInmuebleNavigation.Inmueble == entity.NombreInmueble
                                    && m.FechaSistema.Date == entity.FechaSistema.Date).FirstOrDefault();

                    if (model.IdHistoricoSeguro != 0)
                    {
                        model.ArchivoDigital = openFileDialog.SafeFileName;
                        db.SaveChanges();
                        Trazabilidad("Operaciones", "Seguros", model.NombreInmueble, "Subir archivo", "Ficha Seguros");
                        HistoricosSeguros = db.HistoricoSeguros.Where(m => m.FechaSistema.Date == entity.FechaSistema.Date).ToList();
                    }
                }
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Seguros").FirstOrDefault());
        }

        protected override void ImportData()
        {
            base.ImportData();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Worksheets|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo existingFile = new FileInfo(openFileDialog.FileName);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int colCount = worksheet.Dimension.End.Column; 
                    int rowCount = worksheet.Dimension.End.Row; 

                    if (worksheet.Cells[2, 5].Value != null && worksheet.Cells[2, 6].Value != null)
                    {
                        decimal sumaContinente = 0;

                        try
                        {
                            decimal DañosTotal = Convert.ToDecimal(worksheet.Cells[2, 5].Value);
                            decimal ResponsabilidadTotal = Convert.ToDecimal(worksheet.Cells[2, 6].Value);


                            System.Data.DataTable dt = new System.Data.DataTable();

                            for (int i = 1; i <= colCount; i++)
                            {
                                dt.Columns.Add("F" + (i));
                            }

                            for (int i = 2; i < rowCount; i++)
                            {
                                DataRow row = dt.NewRow();
                                for (int j = 1; j < colCount; j++)
                                {
                                    row[j - 1] = worksheet.Cells[i, j].Value != null ? worksheet.Cells[i, j].Value.ToString() : string.Empty;
                                }
                                if (row[2].ToString() != string.Empty)
                                {
                                    dt.Rows.Add(row);
                                    sumaContinente += Convert.ToDecimal(row[2].ToString());
                                }
                            }

                            HistoricosSeguros = (from DataRow dr in dt.Rows
                                                 select new HistoricoSeguros
                                                 {                                                 
                                                     NombreInmueble = dr[1].ToString(),
                                                     IdInmueble = Convert.ToInt32(dr[0].ToString()),
                                                     Continente = Convert.ToDecimal(dr[2].ToString()),
                                                     PerdidaAlquileres = dr[3].ToString() != string.Empty ? Convert.ToDecimal(dr[3].ToString()) : 0,
                                                     Daños = Math.Round((DañosTotal / sumaContinente) * Convert.ToDecimal(dr[2].ToString()),2),
                                                     ResponsabilidadCivil = Math.Round((ResponsabilidadTotal / sumaContinente) * Convert.ToDecimal(dr[2].ToString()),2),
                                                     FechaSeguro = FechaSeguro != null ? FechaSeguro : DateTime.Now,
                                                     FechaIncorporacion = FechaIncorporacion != null ? FechaIncorporacion : DateTime.Now,
                                                     FechaSistema = DateTime.Now,                                                 
                                                     FechaContable = DateTime.Now,
                                                     IdEmpresa = db.Inmuebles.Where(m => m.IdInmueble == Convert.ToInt32(dr[0].ToString())).FirstOrDefault().IdEmpresa

                                                 }).ToList();

                        }
                        catch (Exception ex)
                        {                            
                            Mensaje = "Revise los datos en el fichero de importación.";                            
                        }

                        if (Mensaje == null || Mensaje == string.Empty)
                        {
                            foreach (HistoricoSeguros item in HistoricosSeguros)
                            {
                                var historico = db.HistoricoSeguros.Where(m => m.IdInmueble == item.IdInmueble
                                                    && m.IdInmuebleNavigation.Inmueble == item.NombreInmueble
                                                    && m.FechaSistema.Date == item.FechaSistema.Date);

                                if (historico.Count() == 0)
                                {
                                    db.HistoricoSeguros.Add(item);
                                    db.SaveChanges();
                                    Trazabilidad("Operaciones", "Seguros", item.NombreInmueble, "Alta", "Ficha Seguros");
                                }
                                else
                                {
                                    Mensaje = "Los datos ya han sido cargados anteriormente.";
                                }

                            }
                        }
                    }
                    else
                    {
                        Mensaje = "Los datos Daños y Responsabilidad Civil deben contener un valor. ";
                    }
                }
            }                
        }
    }
}
