using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaConceptoFacturacionVM : ObservableViewModel, IPageViewModel
    {
        public ConceptoFacturacion entity;
        private HomeConceptoFacturacionVM baseVM;
        public FichaConceptoFacturacionVM(HomeConceptoFacturacionVM baseVM, ConceptoFacturacion entity = null)
        {
            this.entity = entity ?? new ConceptoFacturacion(); 
            this.baseVM = baseVM;
            PageViewModels.Add(new AltaConceptoFacturacionVM(baseVM, this.entity));
            PageViewModels.Add(new ConceptoFacturacionContratosVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Concepto Facturación";
        }

        public string Name
        {
            get
            {
                return "Ficha Concepto de Facturación";
            }
        }

        public IPageViewModel AltaConceptoFacturacion
        {
            get { return Acceso(0, false); }
        }

        public IPageViewModel ConceptoFacturacionContratos
        {
            get { return Acceso(1, false); }
        }

        private IPageViewModel Acceso(int indice, bool AdminRequired)
        {

            if (AdminRequired)
                return _pageViewModels[0];

            return _pageViewModels[indice];
        }
    }
}
