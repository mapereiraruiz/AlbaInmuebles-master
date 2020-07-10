using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeTareaVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeTareaVM()
        {
            PageViewModels.Add(new MantenimientoTareaVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Tareas";
        }

        public string Name
        {
            get
            {
                return "Home Tareas";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeTarea";
            }
        }

        public IPageViewModel MantenimientoTareas
        {
            get { return Acceso("Mantenimiento Tareas", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Tareas").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
