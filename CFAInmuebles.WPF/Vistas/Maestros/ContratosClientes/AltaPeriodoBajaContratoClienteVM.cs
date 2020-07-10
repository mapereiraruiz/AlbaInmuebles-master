using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaPeriodoBajaContratoClienteVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public ContratosClientesBajaTemporal entity;
		public ContratosClientes entitybase;

		private FichaContratoClienteVM baseVM;

		private DateTime? _fechainicio;
		private DateTime? _fechafin;

		private bool _selectedItem;

		public AltaPeriodoBajaContratoClienteVM(FichaContratoClienteVM baseVM, ContratosClientes entitybase, ContratosClientesBajaTemporal entity = null)
        {
            this.entity = entity ?? new ContratosClientesBajaTemporal();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}

        
        public string Name
        {
            get
            {
                 return "Alta Periodo Baja Contrato Cliente";
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

		public Visibility MostrarBotones
        {
            get
            {
				if (entity.IdBajaTemporal == 0)
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


			if (entity.IdBajaTemporal > 0)
			{
                FechaInicio = entity.FechaInicio;
                FechaFin = entity.FechaFin;

                Trazabilidad("Maestros", "Contratos Clientes", entity.IdBajaTemporal.ToString(), "Consulta", "Mantenimiento Periodo Baja Contrato Cliente");
			}
        }


		protected override void ModifyData()
		{
			base.ModifyData();
			string accion = "Update";

			if (Errors.Count == 0)
			{
				var model = db.ContratosClientesBajaTemporal.Find(entity?.IdBajaTemporal);

				if (model == null)
				{
					model = new ContratosClientesBajaTemporal();
					accion = "Alta";
				}

                var contrato = db.ContratosClientes.Find(entitybase.IdContratoCliente); 
                model.FechaInicio = FechaInicio;
                model.FechaFin = FechaFin;
                model.IdContratoClienteNavigation = contrato;

                if (entity.IdBajaTemporal == 0)
                {
                    db.ContratosClientesBajaTemporal.Add(model);
                }

                db.SaveChanges();
                Trazabilidad("Maestros", "Contratos Clientes", model.IdBajaTemporal.ToString(), accion, "Periodo Baja");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Contratos Clientes Periodos Baja").FirstOrDefault());
            }
        }



        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Contratos Clientes Periodos Baja").FirstOrDefault();
            viewmodel = new ContratoClientePeriodosBajaVM(baseVM, entitybase);
            baseVM.CurrentPageViewModel = viewmodel;
        }



		protected bool CheckValidationState<T>(string propertyName, T proposedValue)
		{
			if (propertyName == "Contacto")
			{
				if (Mensaje != null)
					Mensaje = Mensaje.Replace(" * El campo Contacto es obligatorio. ", "");

				if (proposedValue == null)
				{
					SetError(propertyName, "*");
					Mensaje += " * El campo Contacto es obligatorio. ";
					return false;
				}
				else
				{
					SetError(propertyName, String.Empty);
					return true;
				}
			}

			if (propertyName == "TipoFichero")
			{
				if (Mensaje != null)
					Mensaje = Mensaje.Replace(" * El campo Tipo Fichero es obligatorio. ", "");

				if (proposedValue == null)
				{
					SetError(propertyName, "*");
					Mensaje += " * El campo Tipo Fichero es obligatorio. ";
					return false;
				}
				else
				{
					SetError(propertyName, String.Empty);
					return true;
				}
			}

			return true;
		}

	}
}
