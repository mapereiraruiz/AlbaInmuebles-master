using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class IncidenciaMantenimientoPreventivoVM : ObservableViewModel, IPageViewModel
    {
        public Incidencias entity;

        private HomeIncidenciasVM baseVM;
        public IncidenciaMantenimientoPreventivoVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Incidencia Mantenimiento Preventivo";
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            //***var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
            var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.IdInmueble).ToList();

            Mantenimientos = db.Mantenimientos.Where(m => m.FechaEliminacion == null && 
                                                     m.IdTipoMantenimientoNavigation.Valor == "Revisión").ToList();
                                                      //&& inmuebles.Contains(m.IdFichero)).ToList();
        }
    }
}
