using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaNormativasVM : ObservableViewModel, IPageViewModel
    {
        public Normas entity;
        private HomeNormativasVM baseVM;
        public FichaNormativasVM(HomeNormativasVM baseVM, Normas entity = null)
        {
            this.entity = entity ?? new Normas();
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaNormativasVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Normativas";
            }
        }

        public IPageViewModel AltaNormativas
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
