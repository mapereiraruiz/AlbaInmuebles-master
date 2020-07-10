using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class IncidenciaEmpresasVM : ObservableViewModel, IPageViewModel
    {
        public Incidencias entity;

        private HomeIncidenciasVM baseVM;

        public IncidenciaEmpresasVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Incidencia Empresas";
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdIncidencia > 0)
                Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
        }
    }
}
