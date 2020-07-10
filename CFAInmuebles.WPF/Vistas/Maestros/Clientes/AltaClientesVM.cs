using CFAInmuebles.Domain.Models;

using System.Collections.Generic;

using System.Linq;

using System.Windows;
using System.Reflection;

namespace CFAInmuebles.WPF
{
    public class AltaClientesVM : ObservableViewModel, IPageViewModel
    {
        public Terceros entity;

        private HomeClientesVM baseVM;
        private string _cliente;
        private string _nif;
        private string _direccion;
        private string _cuentafinanzas;
        private string _cuentacontable;
        private string _municipio;
        private string _codigopostal;
        private string _provincia;

        private bool _selectedItem;

        public AltaClientesVM(HomeClientesVM baseVM, Terceros entidad = null)
        {
            entity = new Terceros();

            if (entidad != null)
            {
                var properties = typeof(Terceros).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                  .Where(p => !(p.PropertyType.IsInterface && p.PropertyType.IsGenericType &&
                                p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                               && p.CanWrite).ToList();
                properties.ForEach(p =>
                {
                    p.GetValue(entidad);
                    p.SetValue(entity, p.GetValue(entidad));
                });
            }
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Clientes";
            }
        }

        public string Cliente
        {
            get { return _cliente; }
            set
            {
                if (_cliente != value)
                {
                    _cliente = value;
                    RaisePropertyChanged("Cliente");
                }
            }
        }

        public new string Provincia
        {
            get { return _provincia; }
            set
            {
                if (_provincia != value)
                {
                    _provincia = value;
                    RaisePropertyChanged("Provincia");
                }
            }
        }

        public string Nif
        {
            get { return _nif; }
            set
            {
                if (_nif != value)
                {
                    _nif = value;
                    RaisePropertyChanged("Nif");
                }
            }
        }

        public string Direccion
        {
            get { return _direccion; }
            set
            {
                if (_direccion != value)
                {
                    _direccion = value;
                    RaisePropertyChanged("Direccion");
                }
            }
        }

        public string Municipio
        {
            get { return _municipio; }
            set
            {
                if (_municipio != value)
                {
                    _municipio = value;
                    RaisePropertyChanged("Municipio");
                }
            }
        }

        public string CodigoPostal
        {
            get { return _codigopostal; }
            set
            {
                if (_codigopostal != value)
                {
                    _codigopostal = value;
                    RaisePropertyChanged("CodigoPostal");
                }
            }
        }


        public string CuentaFinanzas
        {
            get { return _cuentafinanzas; }
            set
            {
                if (_cuentafinanzas != value)
                {
                    _cuentafinanzas = value;
                    RaisePropertyChanged("CuentaFinanzas");
                }
            }
        }

        public string CuentaContable
        {
            get { return _cuentacontable; }
            set
            {
                if (_cuentacontable != value)
                {
                    _cuentacontable = value;
                    RaisePropertyChanged("CuentaContable");
                }
            }
        }

      
        public bool SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity == null)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

       
        protected override void LoadData()
        {
            base.LoadData();

            Provincias = db.Provincias.Where(m => m.FechaEliminacion == null).ToList();

            if (entity != null)
            {
                Cliente = entity.Nombre;
                Nif = entity.NIF;
                Direccion = entity.Direccion;
                Municipio = entity.Poblacion;
                //CuentaFinanzas = entity.CuentaFianza; 
                CuentaContable = entity.Cuenta;
                Provincia = entity.Provincia;
                CodigoPostal = entity.CodigoPostal;
                Trazabilidad("Maestros", "Clientes", entity.Nombre, "Consulta", "Ficha Clientes");
            }
        }

       
        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Clientes").FirstOrDefault());
        }
 
    } 
}

