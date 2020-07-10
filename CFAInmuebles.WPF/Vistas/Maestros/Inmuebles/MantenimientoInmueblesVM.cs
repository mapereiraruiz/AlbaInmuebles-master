using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoInmueblesVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeInmueblesVM baseVM;
        private Inmuebles _selectedItem;
        private string _inmueble;
        private string _calle;
        private string _municipio;
        public string Name
        {
            get
            {
                return "Mantenimiento Inmuebles";
            }
        }

        public MantenimientoInmueblesVM(HomeInmueblesVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public Inmuebles SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public new string Inmueble
        {
            get { return _inmueble; }
            set
            {
                _inmueble = value;
                RaisePropertyChanged("Inmueble");
            }
        }

        public string Municipio
        {
            get { return _municipio; }
            set
            {
                _municipio = value;
                RaisePropertyChanged("Municipio");
            }
        }

        public string Calle
        {
            get { return _calle; }
            set
            {
                _calle = value;
                RaisePropertyChanged("Calle");
            }
        }

        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData((Inmuebles)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoInmuebles = db.TipoInmuebles.Where (m => m.FechaEliminacion == null).ToList();
            TipoInmuebles.Insert(0, new TipoInmuebles { TipoInmueble = "Seleccione:" });

            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });

            
            SearchData();
            Trazabilidad("Maestros", "Inmuebles", "", "Consulta", "Mantenimiento Inmuebles");
        }
     
        private void ModifyData(Inmuebles entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Inmuebles").FirstOrDefault();
            viewmodel = new FichaInmueblesVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Maestros", "Inmuebles", "", "Búsqueda", "Cadena de consulta: Empresa=" + Empresa + "&Inmueble=" + Inmueble + "&Municipio=" + Municipio 
                + "&Calle=" + Calle + "&Tipo Inmueble=" + TipoInmueble?.TipoInmueble);

            var search = db.Inmuebles.Where(m => m.FechaEliminacion == null).AsQueryable();

            //Comprobamos que inmuebles puede ver el usuario
            if (!UserId.Administrador)
            {
                var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                search = search.Where(m => inmueble.Contains(m.IdInmueble));
            }


            if (Empresa != null && Empresa.Empresa != "Seleccione:")
            {
                search = search.Where(m => m.IdEmpresaNavigation == Empresa);
            }
         
            if (!String.IsNullOrEmpty(Inmueble))
                search = search.Where(m => m.Inmueble.Contains(Inmueble));


            if (!String.IsNullOrEmpty(Municipio))
                search = search.Where(m => m.Municipio.Contains(Municipio));


            if (!String.IsNullOrEmpty(Calle))
                search = search.Where(m => m.Calle.Contains(Calle));


            if (TipoInmueble != null)
            {
                search = search.Where(m => m.IdTipoInmuebleNavigation == TipoInmueble);
            }

          
            Inmuebles = search.ToList();
        }
    }
}

