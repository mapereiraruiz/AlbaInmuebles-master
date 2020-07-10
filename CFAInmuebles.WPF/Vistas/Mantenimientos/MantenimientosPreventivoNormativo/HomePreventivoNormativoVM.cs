using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomePreventivoNormativoVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomePreventivoNormativoVM()
        {
            PageViewModels.Add(new MantenimientoPreventivoNormativoVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Mantenimientos Preventivo y Normativo";
        }

        public string Name
        {
            get
            {
                return "Home Mantenimientos Preventivo y Normativo";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomePreventivoNormativo";
            }
        }

        public IPageViewModel MantenimientoPreventivoNormativo
        {
            get { return Acceso("Mantenimiento Preventivo y Normativo", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Mantenimientos Preventivo y Normativo").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
