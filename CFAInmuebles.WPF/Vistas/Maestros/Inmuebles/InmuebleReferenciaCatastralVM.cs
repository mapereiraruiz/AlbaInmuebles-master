using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleReferenciaCatastralVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;

        protected new ICommand _modifyCommand;

        private ReferenciasCatastrales _selectedItem;

        private FichaInmueblesVM baseVM;
        public InmuebleReferenciaCatastralVM(FichaInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;

        }
        public string Name
        {
            get
            {
                return "Inmueble Referencia Catastral";
            }
        }
        public ReferenciasCatastrales SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((ReferenciasCatastrales)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdInmueble > 0)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
                ReferenciasCatastrales = db.ReferenciasCatastrales.Where(m => inmuebles.Contains(m.IdInmueble ?? 0)).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Referencia Catastral");
            }
        }

        protected void ModifyData(ReferenciasCatastrales entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Inmueble Referencia Catastral").FirstOrDefault();
            viewmodel = new AltaInmuebleReferenciaCatastralVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}