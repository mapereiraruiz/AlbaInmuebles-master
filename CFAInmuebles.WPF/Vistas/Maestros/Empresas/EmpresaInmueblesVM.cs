using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class EmpresaInmueblesVM : ObservableViewModel, IPageViewModel
    {
        public Empresas entity;

        protected new ICommand _modifyCommand;

        private HomeEmpresasVM baseVM;
        private Inmuebles _selectedItem;
        public EmpresaInmueblesVM(HomeEmpresasVM baseVM, Empresas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Empresa Inmuebles";
            }
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

            if (entity.IdEmpresa > 0)
            {
                Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).ToList();
                Trazabilidad("Maestros", "Empresas", entity.Empresa, "Consulta", "Mantenimiento Empresas Inmuebles");

                //Comprobamos que inmuebles puede ver el usuario
                if (!UserId.Administrador)
                {
                    var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    Inmuebles = Inmuebles.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                }
            }
        }
        protected void ModifyData(Inmuebles inmueble)
        {
            HomeInmuebles ventana = new HomeInmuebles();

            HomeInmueblesVM datacontext = new HomeInmueblesVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaInmueblesVM(datacontext, inmueble);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
