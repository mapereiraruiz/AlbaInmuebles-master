using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaArticulosVM : ObservableViewModel, IPageViewModel
    {
        public Articulos entity;
        private HomeArticulosVM baseVM;

        public FichaArticulosVM(HomeArticulosVM baseVM, Articulos entidad = null)
        {
            entity = new Articulos();

            if (entidad != null)
            {
                var properties = typeof(Articulos).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !(p.PropertyType.IsInterface && p.PropertyType.IsGenericType &&
                              p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                             && p.CanWrite).ToList();
                properties.ForEach(p =>
                {
                    p.GetValue(entidad);
                    p.SetValue(entity, p.GetValue(entidad));
                });
            }

            PageViewModels.Add(new AltaArticulosVM(baseVM, entity));           
            PageViewModels.Add(new ArticuloHistorialOcupacionVM(baseVM, entity));
            PageViewModels.Add(new ArticuloIncidenciasVM(baseVM, entity));
            PageViewModels.Add(new ArticuloTareasVM(baseVM, entity));
            PageViewModels.Add(new ArticuloFacturasVM(this, entity));
            PageViewModels.Add(new ArticuloObrasVM(this, entity));
            PageViewModels.Add(new AltaArticuloObrasVM(this, null));
            PageViewModels.Add(new ArticuloHistoricoVM(this, entity));

            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Artículos";
            }
        }

        public IPageViewModel AltaArticulos
        {
            get { return Acceso("Alta Artículos", false); }
        }
     
        public IPageViewModel ArticuloHistorialOcupacion
        {
            get { return Acceso("Artículo Historial Ocupación", false); }
        }

        public IPageViewModel ArticuloIncidencias
        {
            get { return Acceso("Artículo Incidencias", false); }
        }

        public IPageViewModel ArticuloTareas
        {
            get { return Acceso("Artículo Tareas", false); }
        }

        public IPageViewModel ArticuloFacturas
        {
            get { return Acceso("Artículo Facturas", false); }
        }

        public IPageViewModel ArticuloObras
        {
            get { return Acceso("Artículo Obras", false); }
        }

        public IPageViewModel AltaArticuloObras
        {
            get { return Acceso("Alta Artículo Obras", false); }
        }
        public IPageViewModel ArticuloHistorico
        {
            get { return Acceso("Artículo Histórico Modificaciones", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Artículos").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
