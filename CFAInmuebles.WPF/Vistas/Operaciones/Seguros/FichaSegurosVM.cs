﻿using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaSegurosVM : ObservableViewModel, IPageViewModel
    {
        public HistoricoSeguros entity;
        private HomeSegurosVM baseVM;
        public FichaSegurosVM(HomeSegurosVM baseVM, HistoricoSeguros entity = null)
        {
            this.entity = entity ?? new HistoricoSeguros();
            this.baseVM = baseVM;
            PageViewModels.Add(new ImportarSegurosVM(baseVM, this.entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Seguros";
            }
        }

        public IPageViewModel ImportarSeguro
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

