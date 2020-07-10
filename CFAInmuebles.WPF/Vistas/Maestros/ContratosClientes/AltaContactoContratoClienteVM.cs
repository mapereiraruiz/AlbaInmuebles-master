using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaContactoContratoClienteVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public Contactos entity;
		public ContratosClientes entitybase;

		private FichaContratoClienteVM baseVM;

		private string _nif;
		private string _contacto;
		private string _telefono1;
		private string _telefono2;
		private string _telefono3;
		private string _fax;
		private string _mail;
		private bool? _mailing1;
		private bool? _mailing2;
		private bool? _mailing3;
		private bool? _mailing4;
		private bool? _mailing5;
		private bool? _mailing6;
		private bool? _mailing7;
		private bool? _mailing8;
		private bool? _mailing9;
		private bool? _mailing10;
		private DateTime? _fechaeliminacion;
		private DateTime _fechasistema;

		private bool _selectedItem;

		public AltaContactoContratoClienteVM(FichaContratoClienteVM baseVM, ContratosClientes entitybase, Contactos entity = null)
		{
            this.entity = entity ?? new Contactos();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}


		public string Name
        {
            get
            {
                 return "Alta Contacto Contrato Cliente";
            }
        }

        public int MaxNif
        {
            get { return 20; }
        }

        public int MaxContacto
        {
            get { return 150; }
        }

        public int MaxTelefono
        {
            get { return 13; }
        }

        public int MaxMail
        {
            get { return 50; }
        }

        public string Nif
		{
			get { return _nif; }
			set
			{
				if (_nif != value)
				{
					_nif = value;
					RaisePropertyChanged("Nif");
				}
			}
		}

		public string Contacto
		{
			get
			{
				CheckValidationState("Contacto", _contacto);
				return _contacto;
			}
			set
			{
				if (_contacto != value)
				{
					CheckValidationState("Contacto", value);
					_contacto = value;
					RaisePropertyChanged("Contacto");
				}
			}
		}


		public string Telefono1
		{
			get { return _telefono1; }
			set
			{
				if (_telefono1 != value)
				{
					_telefono1 = value;
					RaisePropertyChanged("Telefono1");
				}
			}
		}

		public string Telefono2
		{
			get { return _telefono2; }
			set
			{
				if (_telefono2 != value)
				{
					_telefono2 = value;
					RaisePropertyChanged("Telefono2");
				}
			}
		}

		public string Telefono3
		{
			get { return _telefono3; }
			set
			{
				if (_telefono3 != value)
				{
					_telefono3 = value;
					RaisePropertyChanged("Telefono3");
				}
			}
		}

		public string Fax
		{
			get { return _fax; }
			set
			{
				if (_fax != value)
				{
					_fax = value;
					RaisePropertyChanged("Fax");
				}
			}
		}

		public string Mail
		{
			get { return _mail; }
			set
			{
				if (_mail != value)
				{
					_mail = value;
					RaisePropertyChanged("Mail");
				}
			}
		}

		public bool? Mailing1
		{
			get { return _mailing1; }
			set
			{
				if (_mailing1 != value)
				{
					_mailing1 = value;
					RaisePropertyChanged("Mailing1");
				}
			}
		}

		public bool? Mailing2
		{
			get { return _mailing2; }
			set
			{
				if (_mailing2 != value)
				{
					_mailing2 = value;
					RaisePropertyChanged("Mailing2");
				}
			}
		}

		public bool? Mailing3
		{
			get { return _mailing3; }
			set
			{
				if (_mailing3 != value)
				{
					_mailing3 = value;
					RaisePropertyChanged("Mailing3");
				}
			}
		}

		public bool? Mailing4
		{
			get { return _mailing4; }
			set
			{
				if (_mailing4 != value)
				{
					_mailing4 = value;
					RaisePropertyChanged("Mailing4");
				}
			}
		}

		public bool? Mailing5
		{
			get { return _mailing5; }
			set
			{
				if (_mailing5 != value)
				{
					_mailing5 = value;
					RaisePropertyChanged("Mailing5");
				}
			}
		}

		public bool? Mailing6
		{
			get { return _mailing6; }
			set
			{
				if (_mailing6 != value)
				{
					_mailing6 = value;
					RaisePropertyChanged("Mailing6");
				}
			}
		}

		public bool? Mailing7
		{
			get { return _mailing7; }
			set
			{
				if (_mailing7 != value)
				{
					_mailing7 = value;
					RaisePropertyChanged("Mailing7");
				}
			}
		}

		public bool? Mailing8
		{
			get { return _mailing8; }
			set
			{
				if (_mailing8 != value)
				{
					_mailing8 = value;
					RaisePropertyChanged("Mailing8");
				}
			}
		}

		public bool? Mailing9
		{
			get { return _mailing9; }
			set
			{
				if (_mailing9 != value)
				{
					_mailing9 = value;
					RaisePropertyChanged("Mailing9");
				}
			}
		}

		public bool? Mailing10
		{
			get { return _mailing10; }
			set
			{
				if (_mailing10 != value)
				{
					_mailing10 = value;
					RaisePropertyChanged("Mailing10");
				}
			}
		}

		public DateTime? FechaEliminacion
		{
			get { return _fechaeliminacion; }
			set
			{
				if (_fechaeliminacion != value)
				{
					_fechaeliminacion = value;
					RaisePropertyChanged("FechaEliminacion");
				}
			}
		}

		public DateTime FechaSistema
		{
			get { return _fechasistema; }
			set
			{
				if (_fechasistema != value)
				{
					_fechasistema = value;
					RaisePropertyChanged("FechaSistema");
				}
			}
		}

		public Visibility MostrarBotones
        {
            get
            {
				if (entity.IdContacto == 0)
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

            TipoFicheros = db.TipoFichero.ToList();

			if (entity.IdContacto > 0)
			{
                Nif = entity.Nif;
				Contacto = entity.Contacto;
				Telefono1 = entity.Telefono1;
				Telefono2 = entity.Telefono2;
				Telefono3 = entity.Telefono3;
				Fax = entity.Fax;
				Mail = entity.Mail;
				Mailing1 = entity.Mailing1;
				Mailing2 = entity.Mailing2;
				Mailing3 = entity.Mailing3;
				Mailing4 = entity.Mailing4;
				Mailing5 = entity.Mailing5;
				Mailing6 = entity.Mailing6;
				Mailing7 = entity.Mailing7;
				Mailing8 = entity.Mailing8;
				Mailing9 = entity.Mailing9;
				Mailing10 = entity.Mailing10;

				Trazabilidad("Maestros", "Contratos Clientes", entity.Contacto, "Consulta", "Mantenimiento Contacto Contrato Cliente");
			}
        }


		protected override void ModifyData()
		{
			base.ModifyData();
			string accion = "Update";

			if (Errors.Count == 0)
			{
				var model = db.Contactos.Find(entity?.IdContacto);

				if (model == null)
				{
					model = new Contactos();
					accion = "Alta";
				}

                model.Nif = Nif;             
                model.IdTipoFicheroNavigation = db.TipoFichero.Where(m => m.Valor == "Cliente").FirstOrDefault();
                model.Contacto = Contacto;
                model.Telefono1 = Telefono1;
                model.Telefono2 = Telefono2;
                model.Telefono3 = Telefono3;
                model.Fax = Fax;
                model.Mail = Mail;
                model.Mailing1 = Mailing1;
                model.Mailing2 = Mailing2;
                model.Mailing3 = Mailing3;
                model.Mailing4 = Mailing4;
                model.Mailing5 = Mailing5;
                model.Mailing6 = Mailing6;
                model.Mailing7 = Mailing7;
                model.Mailing8 = Mailing8;
                model.Mailing9 = Mailing9;
                model.Mailing10 = Mailing10;
                model.IdFichero = entitybase.IdContratoCliente;
                model.IdTipoFicheroNavigation = db.TipoFichero.Where(m => m.Valor == "Contrato Cliente").FirstOrDefault();
                model.IdUsuarioNavigation = UserId;
                model.FechaSistema = DateTime.Now;

                if (entity.IdContacto == 0)
                {
                    db.Contactos.Add(model);
                    db.SaveChanges();
                }

                db.SaveChanges();
                Trazabilidad("Maestros", "Contratos Clientes", Contacto, accion, "Contacto");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Contratos Clientes Contactos").FirstOrDefault());
            }
        }



		protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Contacto Contrato Cliente").FirstOrDefault();
                viewmodel = new DeleteContactoContratoClienteVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
        }


        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Contratos Clientes Contactos").FirstOrDefault();
            viewmodel = new ContratoClienteContactosVM(baseVM, entitybase);
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
