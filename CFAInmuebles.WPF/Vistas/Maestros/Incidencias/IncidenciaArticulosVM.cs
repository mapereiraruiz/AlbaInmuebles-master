using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class IncidenciaArticulosVM : ObservableViewModel, IPageViewModel 
    {
        public Incidencias entity;

        private HomeIncidenciasVM baseVM;
        public IncidenciaArticulosVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Incidencia Artículos";
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity != null)
            {
                //***var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.IdInmueble).ToList();
                Articulos = db.Articulos.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
            }
        }
    }
}
