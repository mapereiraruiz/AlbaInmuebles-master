using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaObraVM : ObservableViewModel, IPageViewModel
    {
        public HistorialObra entity;
        private HomeObraVM baseVM;
        public FichaObraVM(HomeObraVM baseVM, HistorialObra entity = null)
        {
            this.entity = entity ?? new HistorialObra();
            this.baseVM = baseVM;

            PageViewModels.Add(new AltaObraVM(baseVM, this.entity));
            PageViewModels.Add(new ObraObrasFicheroVM(this, this.entity));
            PageViewModels.Add(new ObraLicenciasVM(this, this.entity));
            PageViewModels.Add(new ObraTareasVM(this.baseVM, this.entity));
            PageViewModels.Add(new ObraIncidenciasVM(this.baseVM, this.entity));

            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Obra";
            }
        }

        public IPageViewModel AltaObra
        {
            get { return Acceso("Alta Obra", false); }
        }

        public IPageViewModel ObraObrasFichero
        {
            get { return Acceso("Obra ObrasFichero", false); }
        }


        public IPageViewModel ObraLicencias
        {
            get { return Acceso("Obra Licencias", false); }
        }

        public IPageViewModel ObraTareas
        {
            get { return Acceso("Obra Tareas", false); }
        }
        public IPageViewModel ObraIncidencias
        {
            get { return Acceso("Obra Incidencias", false); }
        }



        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Obra").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }

    }
}
