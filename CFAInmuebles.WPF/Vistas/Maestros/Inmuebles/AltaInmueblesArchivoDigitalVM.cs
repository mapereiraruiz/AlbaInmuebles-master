using CFAInmuebles.Domain.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class AltaInmueblesArchivoDigitalVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;

        protected ICommand _openDialogCommand;

        private HomeInmueblesVM baseVM;
        private AltaInmueblesVM ficha;
        private string _archivodigital;

        public AltaInmueblesArchivoDigitalVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            ficha = new AltaInmueblesVM(baseVM, entity);
        }
        public string Name
        {
            get
            {
                return "Alta Inmuebles Archivo Digital";
            }
        }

        public int MaxArchivoDigital
        {
            get { return 50; }
        }
        public ICommand OpenDialogCommand
        {
            get
            {
                if (_openDialogCommand == null)
                {
                    _openDialogCommand = new RelayCommand(p => OpenDialog());
                }
                return _openDialogCommand;
            }
        }
        public string ArchivoDigital
        {
            get { return _archivodigital; }
            set
            {
                if (_archivodigital != value)
                {
                    _archivodigital = value;
                    entity.ArchivoDigital = ArchivoDigital;
                    RaisePropertyChanged("ArchivoDigital");
                }
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            ConceptosFacturacion = db.ConceptoFacturacion.Where(m => m.FechaEliminacion == null).ToList();

            if (entity != null)
            {
                ArchivoDigital = entity.ArchivoDigital;

            }
        }

        protected void OpenDialog()
        {
            var of = new OpenFileDialog();
            of.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            var res = of.ShowDialog();
            if (res.HasValue)
            {
                ArchivoDigital = of.FileName;
            }
        }
    }
}
