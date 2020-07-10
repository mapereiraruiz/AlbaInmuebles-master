using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class IncidenciaInmueblesVM : ObservableViewModel, IPageViewModel
    {
        public Incidencias entity;

        private HomeIncidenciasVM baseVM;
        public IncidenciaInmueblesVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Incidencia Inmuebles";
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdIncidencia > 0)
            {
                Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).ToList();

                //Comprobamos que inmuebles puede ver el usuario
                if (!UserId.Administrador)
                {
                    var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    Inmuebles = Inmuebles.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                }

            }
        }
    }
}
