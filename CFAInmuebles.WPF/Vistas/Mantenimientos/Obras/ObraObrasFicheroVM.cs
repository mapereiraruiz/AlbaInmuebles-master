using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ObraObrasFicheroVM : ObservableViewModel, IPageViewModel
    {
        public HistorialObra entitybase;
        public ObrasFichero entity;

        protected new ICommand _modifyCommand;

        private FichaObraVM baseVM;

        public ObraObrasFicheroVM(FichaObraVM baseVM, HistorialObra entitybase, ObrasFichero entity = null)
        {
            this.entity = entity;
            this.entitybase = entitybase;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Obra ObrasFichero";
            }
        }


        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData((ObrasFichero)p));
                }
                return _modifyCommand;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();

            if (entitybase.IdHistorialObra > 0)
            {
               
                ObrasFichero = db.ObrasFichero.Where(m => m.IdHistorialObra == entitybase.IdHistorialObra).ToList();

                var listInmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdInmueble, i.Inmueble }).ToList();
                var listArticulos = db.Articulos.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdArticulo, i.Articulo }).ToList();
                var listContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(i => new { i.IdContratoCliente, i.NombreCliente }).ToList();

                foreach (var obra in ObrasFichero)
                {
                    switch (obra.IdTipoFicheroNavigation?.Valor)
                    {
                        case "Inmueble":
                            obra.TipoObraDescripcion = listInmuebles.Where(m => m.IdInmueble == obra.IdFichero).FirstOrDefault()?.Inmueble;
                            break;
                        case "Artículo":
                            obra.TipoObraDescripcion = listArticulos.Where(m => m.IdArticulo == obra.IdFichero).FirstOrDefault()?.Articulo;
                            break;
                        case "Contrato Cliente":
                            obra.TipoObraDescripcion = listContratosClientes.Where(m => m.IdContratoCliente == obra.IdFichero).FirstOrDefault()?.NombreCliente;
                            break;
                    }
                }

                Trazabilidad("Mantenimientos", "Obras", entitybase.IdHistorialObra.ToString(), "Consulta", "Mantenimiento Obra ObrasFichero");
            }
        }

        protected void ModifyData(ObrasFichero entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Obras ObrasFichero").FirstOrDefault();
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}