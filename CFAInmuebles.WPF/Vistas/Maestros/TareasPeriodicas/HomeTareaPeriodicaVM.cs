using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeTareaPeriodicaVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeTareaPeriodicaVM()
        {
            PageViewModels.Add(new MantenimientoTareaPeriodicaVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Tarea Periódica";
        }

        public string Name
        {
            get
            {
                return "Home Tarea Periódica";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeTareaPeriodica";
            }
        }

        public IPageViewModel MantenimientoTareaPeriodica
        {
            get { return Acceso("Mantenimiento Tarea Periódica", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Tarea Periódica").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
