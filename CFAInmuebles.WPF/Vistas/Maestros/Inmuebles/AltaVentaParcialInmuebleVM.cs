using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaVentaParcialInmuebleVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public VentaParcialInmueble entity;
		public Inmuebles entitybase;

		private FichaInmueblesVM baseVM;

		private string _comentario;
		private DateTime? _fechaventa;

		private bool _selectedItem;

		public AltaVentaParcialInmuebleVM(FichaInmueblesVM baseVM, Inmuebles entitybase, VentaParcialInmueble entity = null)
		{
            this.entity = entity ?? new VentaParcialInmueble();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}


		public string Name
        {
            get
            {
                 return "Alta Venta Parcial Inmueble";
            }
        }

        public int MaxComentario
        {
            get { return 250; }
        }

		public string Comentario
		{
			get
			{
                return _comentario;
			}
			set
			{
				if (_comentario != value)
				{
                    _comentario = value;
					RaisePropertyChanged("Comentario");
				}
			}
		}

		public DateTime? FechaVenta
		{
			get { return _fechaventa; }
			set
			{
				if (_fechaventa != value)
				{
                    _fechaventa = value;
					RaisePropertyChanged("FechaVenta");
				}
			}
		}

		public Visibility MostrarBotones
        {
            get
            {
				if (entity.IdVentaParcialInmueble == 0)
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

			if (entity.IdVentaParcialInmueble > 0)
			{
                Comentario = entity.Comentario;
                FechaVenta = entity.FechaVenta;
				
				Trazabilidad("Maestros", "Inmuebles", entity.IdVentaParcialInmueble.ToString(), "Consulta", "Mantenimiento Venta Parcial Inmueble");
			}
        }


		protected override void ModifyData()
		{
			base.ModifyData();
			string accion = "Update";

			if (Errors.Count == 0)
			{
				var model = db.VentaParcialInmueble.Find(entity?.IdVentaParcialInmueble);

				if (model == null)
				{
					model = new VentaParcialInmueble();
					accion = "Alta";
				}

                model.Comentario = Comentario;
                model.FechaVenta = FechaVenta;
                model.IdInmueble = entitybase.IdInmueble;
               
                if (entity.IdVentaParcialInmueble == 0)
                {
                    db.VentaParcialInmueble.Add(model);
                }

                db.SaveChanges();
                Trazabilidad("Maestros", "Inmuebles", model.IdVentaParcialInmueble.ToString(), accion, "Venta Parcial Inmueble");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Inmueble Venta Parcial Inmuebles").FirstOrDefault());
            }
        }



		protected override void DeleteData()
        {
            base.DeleteData();

        }


        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Inmueble Ventas Parciales").FirstOrDefault();
            viewmodel = new InmuebleVentasParcialesVM(baseVM, entitybase);
            baseVM.CurrentPageViewModel = viewmodel;
        }



		protected bool CheckValidationState<T>(string propertyName, T proposedValue)
		{
			return true;
		}

	}
}
