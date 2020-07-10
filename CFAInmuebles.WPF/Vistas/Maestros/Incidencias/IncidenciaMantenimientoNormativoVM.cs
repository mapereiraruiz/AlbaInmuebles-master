using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class IncidenciaMantenimientoNormativoVM : ObservableViewModel, IPageViewModel
    {
        public Incidencias entity;

        private HomeIncidenciasVM baseVM;
        public IncidenciaMantenimientoNormativoVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Incidencia Mantenimiento Normativo";
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            //***var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
            var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.IdInmueble).ToList();

            Mantenimientos = db.Mantenimientos.Where(m => m.FechaEliminacion == null && 
                                                     m.IdTipoMantenimientoNavigation.Valor == "Inversión").ToList();
                                                     //&& inmuebles.Contains(m.IdFichero)).ToList();
        }
    }
}
