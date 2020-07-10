using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class HomeObraVM : ObservableViewModel, IWindow, IPageViewModel
    {
        public HomeObraVM()
        {
            PageViewModels.Add(new MantenimientoObraVM(this));
            CurrentPageViewModel = PageViewModels[0];
            Titulo = "Inmobiliaria - Obra";
        }

        public string Name
        {
            get
            {
                return "Home Obra";
            }
        }

        public string WindowName
        {
            get
            {
                return "HomeObra";
            }
        }

        public IPageViewModel MantenimientoObra
        {
            get { return Acceso("Mantenimiento Obra", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Home Obra").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
