using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class EmpresaArticulosVM : ObservableViewModel, IPageViewModel 
    {
        public Empresas entity;

        protected new ICommand _modifyCommand;

        private HomeEmpresasVM baseVM;
        private Articulos _selectedItem;
        public EmpresaArticulosVM(HomeEmpresasVM baseVM, Empresas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Empresa Artículos";
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

            if (entity.IdEmpresa  > 0)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Select(m => m.IdInmueble).ToList();
                Articulos = db.Articulos.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Empresas", entity.Empresa, "Consulta", "Mantenimiento Empresas Artículos");

                //Comprobamos que articulos puede ver el usuario en función del inmueble
                if (!UserId.Administrador)
                {
                    var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    Articulos = Articulos.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                   
                }
            }
        }
        protected void ModifyData(Articulos articulo)
        {
            HomeArticulos ventana = new HomeArticulos();

            HomeArticulosVM datacontext = new HomeArticulosVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaArticulosVM(datacontext, articulo);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
