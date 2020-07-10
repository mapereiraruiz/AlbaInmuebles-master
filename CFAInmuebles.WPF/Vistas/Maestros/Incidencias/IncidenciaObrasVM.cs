using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class IncidenciaObrasVM : ObservableViewModel, IPageViewModel
    {
        public Incidencias entity;

        private HomeIncidenciasVM baseVM;
		public IncidenciaObrasVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
		{
            this.entity = entity;
            this.baseVM = baseVM;
    
        }
        public string Name
        {
            get
            {
                return "Incidencia Obras";
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity != null)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.IdInmueble).ToList();
                var ficheroobras = db.ObrasFichero.Where(m => inmuebles.Contains(m.IdFichero ?? 0) && m.IdTipoFicheroNavigation.Valor == "Inmueble").Select(m => m.IdHistorialObra).ToList();
                Obras = db.HistorialObra.Where(m => m.FechaEliminacion == null && ficheroobras.Contains(m.IdHistorialObra)).ToList();
                Trazabilidad("Maestros", "Incidencias", entity.Incidencia, "Consulta", "Mantenimiento Incidencias Historial Obras");
            }
        }
    }
}