using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoArticulosVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeArticulosVM baseVM;
        private Articulos _selectedItem;
        private string _articulo;

        public string Name
        {
            get
            {
                return "Mantenimiento Artículos";
            }
        }

        public MantenimientoArticulosVM(HomeArticulosVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public string Articulo
        {
            get { return _articulo; }
            set
            {
                if (_articulo != value)
                {
                    _articulo = value;
                    RaisePropertyChanged("Articulo");
                }
            }
        }

        public Articulos SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData((Articulos)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).ToList();

            TipoArticulos = db.TipoArticulos.ToList();
            TipoArticulos.Insert(0, new TipoArticulos { Tipoarticulo = "Seleccione:" });

            Articulos = db.Articulos.Where(m => m.FechaEliminacion == null).ToList();
            Trazabilidad("Maestros", "Artículos", "", "Consulta", "Mantenimiento Artículos");

            //Comprobamos que artículos puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                Articulos = Articulos.Where(m => inmuebles.Contains(m.IdInmueble)).ToList();
                Inmuebles = Inmuebles.Where(m => inmuebles.Contains(m.IdInmueble)).ToList();
            }

            Inmuebles.Insert(0, new Inmuebles { Inmueble = "Seleccione:" });
            SearchData();
        }

        protected void ModifyData(Articulos entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Artículos").FirstOrDefault();
            viewmodel = new FichaArticulosVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Maestros", "Artículos", "", "Búsqueda", "Cadena de consulta: Artículo=" + Articulo + "&Inmueble=" + Inmueble?.Inmueble
                + "&Tipo Artículo=" + TipoArticulo?.Tipoarticulo);

            var search = db.Articulos.Where(m => m.FechaEliminacion == null).AsQueryable();

            //Comprobamos que artículos puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                search = search.Where(m => inmuebles.Contains(m.IdInmueble));
            }

            if (!String.IsNullOrEmpty(Articulo))
                search = search.Where(m => m.Articulo.Contains(Articulo));

            if (Inmueble != null && Inmueble.Inmueble != "Seleccione:")
            {
                search = search.Where(m => m.IdInmuebleNavigation == Inmueble);
            }


            if (TipoArticulo != null && TipoArticulo.Tipoarticulo != "Seleccione:")
            {
                search = search.Where(m => m.IdTipoArticuloNavigation == TipoArticulo);
            }

            Articulos = search.ToList();
        }
    }
}
