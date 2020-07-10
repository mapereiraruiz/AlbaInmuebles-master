using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class FichaArticuloHistoricoVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public HistoricoArticulos entity;
		public Articulos entitybase;

		private FichaArticulosVM baseVM;

        private bool _selectedItem;

		public FichaArticuloHistoricoVM(FichaArticulosVM baseVM, Articulos entitybase, HistoricoArticulos entity = null)
		{
            this.entity = entity ?? new HistoricoArticulos();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}


		public string Name
        {
            get
            {
                 return "Ficha Artículo Histórico";
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
				if (entity.IdHistoricoArticulo == 0)
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

			if (entity.IdHistoricoArticulo > 0)
			{
                Trazabilidad("Maestros", "Artículos", entity.Articulo, "Consulta", "Mantenimiento Artículo Histórico");
			}
        }

        protected override void VolverListado()
        {
			base.VolverListado();
			var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Artículo Histórico Modificación").FirstOrDefault();
			viewmodel = new ArticuloHistoricoVM(baseVM, entitybase);
			baseVM.CurrentPageViewModel = viewmodel;
		}
	}
}
