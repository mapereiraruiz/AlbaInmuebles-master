using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class InmuebleVM : ObservableViewModel, IPageViewModel
    {
       
        public InmuebleVM()
        {
          //  PageViewModels.Add(new EmpresaArticulosVM());
         //   CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Inmueble";
            }
        }
    }
}
