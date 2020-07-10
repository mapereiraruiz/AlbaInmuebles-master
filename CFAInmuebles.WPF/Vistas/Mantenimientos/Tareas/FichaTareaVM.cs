using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaTareaVM : ObservableViewModel, IPageViewModel
    {
        public Tareas entity;
        private HomeTareaVM baseVM;
        public FichaTareaVM(HomeTareaVM baseVM, Tareas entity = null)
        {
            this.entity = entity ?? new Tareas();
            this.baseVM = baseVM;           
            PageViewModels.Add(new AltaTareaVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Tarea";
            }
        }

        public IPageViewModel AltaTarea
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
