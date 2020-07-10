using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaPreventivoNormativoVM : ObservableViewModel, IPageViewModel
    {
        public Mantenimientos entity;
        private HomePreventivoNormativoVM baseVM;
        
        
        public FichaPreventivoNormativoVM(HomePreventivoNormativoVM baseVM, Mantenimientos entity = null)
        {
            this.entity = entity ?? new Mantenimientos();
            this.baseVM = baseVM;           
            PageViewModels.Add(new AltaPreventivoNormativoVM(baseVM, this.entity));

            PageViewModels.Add(new MantenimientoPreventivoNormativoTareasVM(this.baseVM, this.entity));
            PageViewModels.Add(new MantenimientoPreventivoNormativoIncidenciasVM(this.baseVM, this.entity));

            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Mantenimientos Preventivo y Normativo";
            }
        }

        //public IPageViewModel AltaPreventivoNormativo
        //{
        //    get { return Acceso(0, false); }
        //}

        public IPageViewModel AltaPreventivoNormativo
        {
            get { return Acceso("Alta PreventivoNormativo", false); }
        }


        public IPageViewModel MantenimientoPreventivoNormativoTareas
        {
            get { return Acceso("Mantenimiento Preventivo Normativo Tareas", false); }
        }
        public IPageViewModel MantenimientoPreventivoNormativoIncidencias
        {
            get { return Acceso("Mantenimiento Preventivo Normativo Incidencias", false); }
        }



        //private IPageViewModel Acceso(int indice, bool AdminRequired)
        //{

        //    if (AdminRequired)
        //        return _pageViewModels[0];

        //    return _pageViewModels[indice];
        //}


        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Mantenimientos Preventivo y Normativo").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }



    }
}
