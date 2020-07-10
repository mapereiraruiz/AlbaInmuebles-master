using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaTareaPeriodicaVM : ObservableViewModel, IPageViewModel
    {
        public TareaPeriodica entity;
        private HomeTareaPeriodicaVM baseVM;
        public FichaTareaPeriodicaVM(HomeTareaPeriodicaVM baseVM, TareaPeriodica entity = null)
        {
            this.entity = entity ?? new TareaPeriodica();
            this.baseVM = baseVM;           
            PageViewModels.Add(new AltaTareaPeriodicaVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Tarea Periódica";
            }
        }

        public IPageViewModel AltaTareaPeriodica
        {
            get { return Acceso(0, false); }
        }

        private IPageViewModel Acceso(int indice, bool AdminRequired)
        {

            if (AdminRequired)
                return _pageViewModels[0];

            return _pageViewModels[indice];
        }
    }
}
