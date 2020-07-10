using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class IncidenciaContratosProveedoresVM : ObservableViewModel, IPageViewModel
    {
        public Incidencias entity;

        private HomeIncidenciasVM baseVM;
        public IncidenciaContratosProveedoresVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Incidencia Contratos Proveedores";
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity != null)
            {
                //***var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.IdInmueble).ToList();
                ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
            }
        }
    }
}