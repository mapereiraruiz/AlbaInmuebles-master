using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MainWindowVM : ObservableViewModel
    {
        public MainWindowVM()
        {
            PageViewModels.Add(new IncidenciasyTareasVM());

            // Maestros

            AppWindows.Add(new HomeEmpresasVM());          
            AppWindows.Add(new HomeInmueblesVM());
            AppWindows.Add(new HomeClientesVM());
            AppWindows.Add(new HomeArticulosVM());
            AppWindows.Add(new HomeContratoClienteVM());
            AppWindows.Add(new HomeContratoProveedorVM());
            AppWindows.Add(new HomeProveedoresVM());
            AppWindows.Add(new HomeIncidenciasVM());
            AppWindows.Add(new HomeTipoIPCVM());
            AppWindows.Add(new HomeProvinciasVM());
            AppWindows.Add(new HomeConceptoFacturacionVM());
            AppWindows.Add(new HomeFormasDePagoVM());
            AppWindows.Add(new HomeTipoArticuloVM());
            AppWindows.Add(new HomeGastosVM());
            AppWindows.Add(new HomeTipoIVAVM());
            AppWindows.Add(new HomeSwiftVM());
            AppWindows.Add(new HomeNormativasVM());
            AppWindows.Add(new HomeTareaVM());
            AppWindows.Add(new HomeTareaPeriodicaVM());
            AppWindows.Add(new HomeLicenciaVM());
            AppWindows.Add(new HomeTipoLicenciaVM());
            AppWindows.Add(new HomeTipoInstalacionVM());
            AppWindows.Add(new HomePreventivoNormativoVM());
            AppWindows.Add(new HomeObraVM());

            // Procesos
            AppWindows.Add(new HomeFacturacionVM());

            //Informes - Inventarios
            AppWindows.Add(new HomeListadoAltasVM());
            AppWindows.Add(new HomeListadoBajasVM());
            AppWindows.Add(new HomeFacturacionDetalladaVM());
            AppWindows.Add(new HomeFacturacionAnualVM());

            //Operaciones
            AppWindows.Add(new HomeSegurosVM());


            CurrentPageViewModel = PageViewModels[0];
            CurrentWindowOpen = AppWindows[0];
            Titulo = "Inmobiliaria";
        }


        public IPageViewModel IncidenciasyTareas
        {
            get { return _pageViewModels[0]; }
        }

        public IWindow HomeEmpresas
        {
            get { return Open("HomeEmpresas", false); }
        }

        public IWindow HomeInmuebles
        {
            get { return Open("HomeInmuebles", false); }
        }

        public IWindow HomeArticulos
        {
            get { return Open("HomeArticulos", false); }
        }

        public IWindow HomeContratoCliente
        {
            get { return Open("HomeContratoCliente", false); }
        }

        public IWindow HomeContratoProveedor
        {
            get { return Open("HomeContratoProveedor", false); }
        }

        public IWindow HomeClientes
        {
            get { return Open("HomeClientes", false); }
        }

        public IWindow HomeProveedores
        {
            get { return Open("HomeProveedores", false); }
        }

        public IWindow HomeIncidencias
        {
            get { return Open("HomeIncidencias", false); }
        }

        public IWindow HomeTipoIPC
        {
            get { return Open("HomeTipoIPC", false); }
        }

        public IWindow HomeProvincias
        {
            get { return Open("HomeProvincias", false); }
        }

        public IWindow HomeConceptoFacturacion
        {
            get { return Open("HomeConceptoFacturacion", false); }
        }

        public IWindow HomeFormasDePago
        {
            get { return Open("HomeFormasDePago", false); }
        }

        public IWindow HomeTipoArticulo
        {
            get { return Open("HomeTipoArticulo", false); }
        }

        public IWindow HomeGastos
        {
            get { return Open("HomeGastos", false); }
        }

        public IWindow HomeTipoIVA
        {
            get { return Open("HomeTipoIVA", false); }
        }

        public IWindow HomeSwift
        {
            get { return Open("HomeSwift", false); }
        }

        public IWindow HomeNormativas     {
            get { return Open("HomeNormativas", false); }
        }

        public IWindow HomeTarea
        {
            get { return Open("HomeTarea", false); }
        }

        public IWindow HomeTareaPeriodica
        {
            get { return Open("HomeTareaPeriodica", false); }
        }

        public IWindow HomeLicencia
        {
            get { return Open("HomeLicencia", false); }
        }

        public IWindow HomeTipoLicencia
        {
            get { return Open("HomeTipoLicencia", false); }
        }

        public IWindow HomeTipoInstalacion
        {
            get { return Open("HomeTipoInstalacion", false); }
        }

        public IWindow HomePreventivoNormativo
        {
            get { return Open("HomePreventivoNormativo", false); }
        }

        public IWindow HomeObra
        {
            get { return Open("HomeObra", false); }
        }

        public IWindow HomeFacturacion
        {
            get { return Open("HomeFacturacion", false); }
        }

        public IWindow HomeListadoAltas
        {
            get { return Open("HomeListadoAltas", false); }
        }

        public IWindow HomeListadoBajas
        {
            get { return Open("HomeListadoBajas", false); }
        }
        public IWindow HomeFacturacionDetallada       
        {
            get { return Open("HomeFacturacionDetallada", false); }
        }

        public IWindow HomeFacturacionAnual
        {
            get { return Open("HomeFacturacionAnual", false); }
        }

        public IWindow HomeSeguros
        {
            get { return Open("HomeSeguros", false); }
        }

        private IPageViewModel Acceso(int indice, bool AdminRequired)
        {
            {

                if (AdminRequired)
                    return _pageViewModels[0];

                return _pageViewModels[indice];
            }
        }

        public IWindow Open(string viewModel, bool AdminRequired)
        {

            if (AdminRequired)
                return AppWindows.Where(m => m.WindowName == "HomeTipoIPC").FirstOrDefault();

            return AppWindows.Where(m => m.WindowName == viewModel).FirstOrDefault();
        }
    }
}
