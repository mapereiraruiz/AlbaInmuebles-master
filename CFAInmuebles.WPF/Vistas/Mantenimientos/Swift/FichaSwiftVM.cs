using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaSwiftVM : ObservableViewModel, IPageViewModel
    {
        public Swift entity;
        private HomeSwiftVM baseVM;
        public FichaSwiftVM(HomeSwiftVM baseVM, Swift entity = null)
        {
            this.entity = entity ?? new Swift();
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaSwiftVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Swift";
            }
        }

        public IPageViewModel AltaSwift
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