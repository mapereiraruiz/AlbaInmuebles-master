﻿using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoProveedorIncidenciasVM : ObservableViewModel, IPageViewModel
    {
        public ContratosProveedores entity;

        protected new ICommand _modifyCommand;

        private HomeContratoProveedorVM baseVM;
        private Incidencias _selectedItem;
        public ContratoProveedorIncidenciasVM(HomeContratoProveedorVM baseVM, ContratosProveedores entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Proveedores Incidencias";
            }
        }

        public Incidencias SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Incidencias)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoProveedor > 0)
            {
                Incidencias = db.Incidencias.Where(m => m.FechaEliminacion == null && m.IdTipoFicheroNavigation.Valor == "Contrato Proveedor"  && m.IdFichero == entity.IdContratoProveedor).ToList();

                Trazabilidad("Maestros", "Contratos Proveedores", entity.ReferenciaContrato, "Consulta", "Mantenimiento Contratos Proveedores Incidencias");
            }
        }
        protected void ModifyData(Incidencias entity)
        {
            HomeIncidencias ventana = new HomeIncidencias();

            HomeIncidenciasVM datacontext = new HomeIncidenciasVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaIncidenciasVM(datacontext, entity);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
