using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaDatosImprimirContratoClienteVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public DatosImprimirFactura entity;
		public ContratosClientes entitybase;

		private FichaContratoClienteVM baseVM;

		private DateTime? _fechainicio;
		private DateTime? _fechafin;
        private string _nota;

		private bool _selectedItem;

		public AltaDatosImprimirContratoClienteVM(FichaContratoClienteVM baseVM, ContratosClientes entitybase, DatosImprimirFactura entity = null)
        {
            this.entity = entity ?? new DatosImprimirFactura();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}

        public int MaxNota
        {
            get
            {
                return 200;
            }
        }

        public string Name
        {
            get
            {
                 return "Alta Datos Imprimir Contrato Cliente";
            }
        }
      
		public DateTime? FechaInicio
		{
			get { return _fechainicio; }
			set
			{
				if (_fechainicio != value)
				{
                    _fechainicio = value;
					RaisePropertyChanged("FechaInicio");
				}
			}
		}

		public DateTime? FechaFin
		{
			get { return _fechafin; }
			set
			{
				if (_fechafin != value)
				{
                    _fechafin = value;
					RaisePropertyChanged("FechaFin");
				}
			}
		}

        public string Nota
        {
            get { return _nota; }
            set
            {
                if (_nota != value)
                {
                    _nota = value;
                    RaisePropertyChanged("Nota");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
				if (entity.IdDatosImprimir == 0)
				{
					return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

		public bool SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;
				RaisePropertyChanged("SelectedItem");
			}
		}

		protected override void LoadData()
        {
            base.LoadData();


			if (entity.IdDatosImprimir > 0)
			{
                FechaInicio = entity.FechaInicio;
                FechaFin = entity.FechaFin;
                Nota = entity.Nota;

                Trazabilidad("Maestros", "Contratos Clientes", entity.Nota, "Consulta", "Mantenimiento Datos Imprimir Contrato Cliente");
			}
        }


		protected override void ModifyData()
		{
			base.ModifyData();
			string accion = "Update";

			if (Errors.Count == 0)
			{
				var model = db.DatosImprimirFactura.Find(entity?.IdDatosImprimir);

				if (model == null)
				{
					model = new DatosImprimirFactura();
					accion = "Alta";
				}

                var contrato = db.ContratosClientes.Find(entitybase.IdContratoCliente); 
                model.FechaInicio = FechaInicio;
                model.FechaFin = FechaFin;
                model.Nota = Nota;
                model.IdContratoClienteNavigation = contrato;

                if (entity.IdDatosImprimir == 0)
                {
                    db.DatosImprimirFactura.Add(model);
                }

                db.SaveChanges();
                Trazabilidad("Maestros", "Contratos Clientes", model.Nota, accion, "Datos Imprimir");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Contratos Clientes Datos Imprimir").FirstOrDefault());
            }
        }



        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Contratos Clientes Datos Imprimir").FirstOrDefault();
            viewmodel = new ContratoClienteDatosImprimirVM(baseVM, entitybase);
            baseVM.CurrentPageViewModel = viewmodel;
        }

	}
}
