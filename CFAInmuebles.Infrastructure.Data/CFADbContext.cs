using CFAInmuebles.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace CFAInmuebles.Infrastructure.Data
{
    public interface IDbContextSchema
    {
        string Schema { get; }
    }

    public class CFADbContext : DbContext, IDbContextSchema
    {
        public string Schema { get; }
        public CFADbContext()
        {
        }

        public CFADbContext(DbContextOptions<CFADbContext> options, String schemma = null)
            : base(options)
        {
            Schema = schemma;
        }

        public virtual DbSet<Terceros> Terceros { get; set; }

        public virtual DbSet<Apuntes> Apuntes { get; set; }
        public virtual DbSet<Articulos> Articulos { get; set; }
        public virtual DbSet<CatalogarConcepto> CatalogarConcepto { get; set; }
        public virtual DbSet<Clasificacion> Clasificacion { get; set; }
        public virtual DbSet<ConceptoFacturacion> ConceptoFacturacion { get; set; }
        public virtual DbSet<ConceptoFacturacionPadre> ConceptoFacturacionPadre { get; set; }
        public virtual DbSet<Contactos> Contactos { get; set; }
        public virtual DbSet<ContratosClientes> ContratosClientes { get; set; }
        public virtual DbSet<ContratosClientesBajaTemporal> ContratosClientesBajaTemporal { get; set; }
        public virtual DbSet<ContratosProveedores> ContratosProveedores { get; set; }
        public virtual DbSet<DatosImprimirFactura> DatosImprimirFactura { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<Facturacion> Facturacion { get; set; }
        public virtual DbSet<FormasDePago> FormasDePago { get; set; }
        public virtual DbSet<Gastos> Gastos { get; set; }
        public virtual DbSet<GastosAnuales> GastosAnuales { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<HistorialObra> HistorialObra { get; set; }
        public virtual DbSet<HistoricoConceptoFacturacion> HistoricoConceptoFacturacion { get; set; }
        public virtual DbSet<HistoricoFacturacion> HistoricoFacturacion { get; set; }
        public virtual DbSet<HistoricoFacturacionBanco> HistoricoFacturacionBanco { get; set; }
        public virtual DbSet<HistoricoFacturacionSuperficie> HistoricoFacturacionSuperficie { get; set; }
        public virtual DbSet<HistoricoSeguros> HistoricoSeguros { get; set; }
        public virtual DbSet<HistoricoTasaciones> HistoricoTasaciones { get; set; }
        public virtual DbSet<Incidencias> Incidencias { get; set; }
        public virtual DbSet<InmuebleCentroCoste> InmuebleCentroCoste { get; set; }
        public virtual DbSet<Inmuebles> Inmuebles { get; set; }
        public virtual DbSet<Licencias> Licencias { get; set; }
        public virtual DbSet<Mantenimientos> Mantenimientos { get; set; }
        public virtual DbSet<Normas> Normas { get; set; }
        public virtual DbSet<ObrasFichero> ObrasFichero { get; set; }
        public virtual DbSet<Periodicidad> Periodicidad { get; set; }
        public virtual DbSet<PeriodicidadServicio> PeriodicidadServicio { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<ReferenciasCatastrales> ReferenciasCatastrales { get; set; }
        public virtual DbSet<SuperficieTasacion> SuperficieTasacion { get; set; }
        public virtual DbSet<Swift> Swift { get; set; }
        public virtual DbSet<TareaPeriodica> TareaPeriodica { get; set; }
        public virtual DbSet<Tareas> Tareas { get; set; }
        public virtual DbSet<TipoArticulos> TipoArticulos { get; set; }
        public virtual DbSet<TipoConcepto> TipoConcepto { get; set; }
        public virtual DbSet<TipoConceptoArticulo> TipoConceptoArticulo { get; set; }
        public virtual DbSet<TipoFichero> TipoFichero { get; set; }
        public virtual DbSet<TipoGasto> TipoGasto { get; set; }
        public virtual DbSet<TipoInmuebles> TipoInmuebles { get; set; }
        public virtual DbSet<TipoInstalacion> TipoInstalacion { get; set; }
        public virtual DbSet<TipoIpc> TipoIpc { get; set; }
        public virtual DbSet<TipoIva> TipoIva { get; set; }
        public virtual DbSet<TipoLicencia> TipoLicencia { get; set; }
        public virtual DbSet<TipoMantenimiento> TipoMantenimiento { get; set; }
        public virtual DbSet<TipoMedida> TipoMedida { get; set; }
        public virtual DbSet<TipoPlanta> TipoPlanta { get; set; }
        public virtual DbSet<UsuarioInmueble> UsuarioInmueble { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<ContratoClienteConceptoFactura> ContratoClienteConceptoFactura { get; set; }
        public virtual DbSet<HistorialOcupacion> HistorialOcupacion { get; set; }
        public virtual DbSet<TipoObra> TipoObra { get; set; }
        public virtual DbSet<TipoOrigen> TipoOrigen { get; set; }
        public virtual DbSet<ModalidadFactura> ModalidadFactura { get; set; }
        public virtual DbSet<HistoricoEmpresas> HistoricoEmpresas { get; set; }
        public virtual DbSet<HistoricoInmuebles> HistoricoInmuebles { get; set; }
        public virtual DbSet<HistoricoContratosClientes> HistoricoContratosClientes { get; set; }
        public virtual DbSet<HistoricoContratosProveedores> HistoricoContratosProveedores { get; set; }
        public virtual DbSet<HistoricoArticulos> HistoricoArticulos { get; set; }
        public virtual DbSet<VentaParcialInmueble> VentaParcialInmueble { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Articulos>(entity =>
            {
                entity.Property(e => e.Articulo).IsUnicode(false);

                entity.Property(e => e.Estado).IsUnicode(false);

                entity.Property(e => e.HistorialObra).IsUnicode(false);

                entity.Property(e => e.Planos).IsUnicode(false);

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulos_Inmueble");

                entity.HasOne(d => d.IdPlantaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdPlanta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulos_Planta");

                entity.HasOne(d => d.IdTipoArticuloNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdTipoArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulos_TipoArt");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Articulos_Usuario");
            });

            modelBuilder.Entity<CatalogarConcepto>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<Terceros>()
                .ToTable("Terceros", Schema);

            modelBuilder.Entity<Apuntes>()
                .ToTable("Apuntes", Schema);

            modelBuilder.Entity<Clasificacion>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Usuario).IsUnicode(false);

                entity.Property(e => e.Mantenimiento).IsUnicode(false);

                entity.Property(e => e.Accion).IsUnicode(false);

                
            });

            modelBuilder.Entity<ConceptoFacturacion>(entity =>
            {
                entity.Property(e => e.Conceptofacturacion1).IsUnicode(false);

                entity.Property(e => e.CuentaContable).IsUnicode(false);

                entity.Property(e => e.CuentaGasto).IsUnicode(false);

                entity.HasOne(d => d.IdCatalogarConceptoNavigation)
                    .WithMany(p => p.ConceptoFacturacion)
                    .HasForeignKey(d => d.IdCatalogarConcepto)
                    .HasConstraintName("FK_ConceptoFacturacion_CatalogarConcepto");

                entity.HasOne(d => d.IdConceptoFacturacionPadreNavigation)
                    .WithMany(p => p.ConceptoFacturacion)
                    .HasForeignKey(d => d.IdConceptoFacturacionPadre)
                    .HasConstraintName("FK_ConceptoFacturacion_ConceptoFacturacionPadre");

                entity.HasOne(d => d.IdTipoConceptoNavigation)
                    .WithMany(p => p.ConceptoFacturacion)
                    .HasForeignKey(d => d.IdTipoConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConceptoFacturacion_TipoConcepto");

                entity.HasOne(d => d.IdTipoIvaNavigation)
                    .WithMany(p => p.ConceptoFacturacion)
                    .HasForeignKey(d => d.IdTipoIva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_conceptoFacturacion_tipoIva");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ConceptoFacturacion)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_ConceptoFacturacion_Usuario");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.ConceptoFacturacion)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_ConceptoFacturacion_Empresa");
            });

            modelBuilder.Entity<ConceptoFacturacionPadre>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<Contactos>(entity =>
            {
                entity.Property(e => e.Contacto).IsFixedLength();

                entity.Property(e => e.Fax).IsUnicode(false);

                entity.Property(e => e.Mail).IsUnicode(false);

                entity.Property(e => e.Nif).IsUnicode(false);

                entity.Property(e => e.Telefono1).IsUnicode(false);

                entity.Property(e => e.Telefono2).IsUnicode(false);

                entity.Property(e => e.Telefono3).IsUnicode(false);

                entity.Property(e => e.IdTipoFichero).IsUnicode(false);


                entity.HasOne(d => d.IdTipoFicheroNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.IdTipoFichero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contactos_TipoFichero");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Contactos_Usuario");
            });

            modelBuilder.Entity<ContratosClientesBajaTemporal>(entity =>
            {
                entity.HasOne(d => d.IdContratoClienteNavigation)
                   .WithMany(p => p.ContratosClientesBajaTemporal)
                   .HasForeignKey(d => d.IdContratoCliente)
                   .HasConstraintName("FK_ContratosClientesBajaTemporal_ContratoCliente");
            });



            modelBuilder.Entity<ContratosClientes>(entity =>
            {
                entity.Property(e => e.AgruparContrato).IsUnicode(false);

                entity.Property(e => e.ArchivoDigital).IsUnicode(false);

                entity.Property(e => e.Comentario).IsUnicode(false);

                entity.Property(e => e.CuentaFianza).IsUnicode(false);

                entity.Property(e => e.DireccionEnvio).IsUnicode(false);

                entity.Property(e => e.NIF).IsUnicode(false);

                entity.HasOne(d => d.IdConceptoFacturacionNavigation)
                    .WithMany(p => p.ContratosClientes)
                    .HasForeignKey(d => d.IdConceptoFacturacion)
                    .HasConstraintName("FK_ContratosClientes_Concepto");

                entity.HasOne(d => d.IdFormaPagoNavigation)
                    .WithMany(p => p.ContratosClientes)
                    .HasForeignKey(d => d.IdFormaPago)
                    .HasConstraintName("FK_ContratosClientes_FormaPago");

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.ContratosClientes)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContratosClientes_Inmueble");

                entity.HasOne(d => d.IdEmpresaNavigation)
                   .WithMany(p => p.ContratosClientes)
                   .HasForeignKey(d => d.IdEmpresa)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_ContratosClientes_Empresa");

                entity.HasOne(d => d.IdSwiftNavigation)
                    .WithMany(p => p.ContratosClientes)
                    .HasForeignKey(d => d.IdSwift)
                    .HasConstraintName("FK_ContratosClientes_Swift");

                entity.HasOne(d => d.IdTipoIpcNavigation)
                    .WithMany(p => p.ContratosClientes)
                    .HasForeignKey(d => d.IdTipoIpc)
                    .HasConstraintName("FK_ContratosClientes_TiposIPC");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ContratosClientes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_ContratosClientes_Usuario");
            });

            modelBuilder.Entity<ContratosProveedores>(entity =>
            {
                entity.Property(e => e.ArchivoDigital).IsUnicode(false);

                entity.Property(e => e.ReferenciaContrato).IsUnicode(false);

                entity.Property(e => e.Servicio).IsUnicode(false);

                entity.Property(e => e.Varios1).IsUnicode(false);

                entity.Property(e => e.Varios2).IsUnicode(false);

                entity.Property(e => e.Varios3).IsUnicode(false);

                entity.Property(e => e.Varios4).IsUnicode(false);

                entity.Property(e => e.Varios5).IsUnicode(false);

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.ContratosProveedores)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contratoProveedor_inmueble");

                entity.HasOne(d => d.IdInmuebleCentroCosteNavigation)
                    .WithMany(p => p.ContratosProveedores)
                    .HasForeignKey(d => d.IdInmuebleCentroCoste)
                    .HasConstraintName("FK_ContratosProveedores_CentroCoste");


                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ContratosProveedores)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_ContratosProveedores_Usuario");

                entity.HasOne(d => d.IdPeriodicidadCosteNavigation)
                   .WithMany(p => p.ContratosProveedoresCoste)
                   .HasForeignKey(d => d.IdPeriodicidadCoste)
                   .HasConstraintName("FK_ContratosProveedores_PeriodicidadCoste");

                entity.HasOne(d => d.IdPeriodicidadRevisionNavigation)
                  .WithMany(p => p.ContratosProveedoresRevision)
                  .HasForeignKey(d => d.IdPeriodicidadRevision)
                  .HasConstraintName("FK_ContratosProveedores_PeriodicidadRevision");
            });

            modelBuilder.Entity<DatosImprimirFactura>(entity =>
            {
                entity.Property(e => e.Nota).IsUnicode(false);

                entity.HasOne(d => d.IdContratoClienteNavigation)
                    .WithMany(p => p.DatosImprimirFactura)
                    .HasForeignKey(d => d.IdContratoCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DatosImprimirFactura_ContratoCliente");
            });

            modelBuilder.Entity<Empresas>(entity =>
            {
                entity.Property(e => e.CodigoSepa).IsUnicode(false);

                entity.Property(e => e.CuentaBancaria).IsUnicode(false);

                entity.Property(e => e.CuentaContable).IsUnicode(false);

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.Empresa).IsUnicode(false);

                entity.Property(e => e.LateralFactura).IsUnicode(false);

                entity.Property(e => e.Nif).IsUnicode(false);

                entity.Property(e => e.PieFactura).IsUnicode(false);

                entity.Property(e => e.PolizaSeguro).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Empresas_Usu");

                entity.HasOne(d => d.IdSwiftNavigation)
                   .WithMany(p => p.Empresas)
                   .HasForeignKey(d => d.IdSwift)
                   .HasConstraintName("FK_Empresas_Swift");
            });

            modelBuilder.Entity<HistoricoEmpresas>(entity =>
            {
                entity.HasOne(d => d.IdEmpresaNavigation)
                   .WithMany(p => p.HistoricoEmpresa)
                   .HasForeignKey(d => d.IdEmpresa)
                   .HasConstraintName("FK_historicoempresa_Empresa");

                entity.HasOne(d => d.IdSwiftNavigation)
                  .WithMany(p => p.HistoricoEmpresa)
                  .HasForeignKey(d => d.IdSwift)
                  .HasConstraintName("FK_historicoempresa_Swift");


                entity.HasOne(d => d.IdUsuarioNavigation)
                   .WithMany(p => p.HistoricoEmpresas)
                   .HasForeignKey(d => d.IdUsuario)
                   .HasConstraintName("FK_historicoempresa_Usuario");
            });

            modelBuilder.Entity<HistoricoInmuebles>(entity =>
            {
                entity.HasOne(d => d.IdInmuebleNavigation)
                   .WithMany(p => p.HistoricoInmueble)
                   .HasForeignKey(d => d.IdInmueble)
                   .HasConstraintName("FK_historicoinmueble_Inmueble");

                entity.HasOne(d => d.IdConceptoFacturacionNavigation)
                    .WithMany(p => p.HistoricoInmueble)
                    .HasForeignKey(d => d.IdConceptoFacturacion)
                    .HasConstraintName("FK_historicoinmueble_Concepto");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.HistoricoInmueble)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historicoinmueble_empresa");

                entity.HasOne(d => d.IdInmuebleCentroCosteNavigation)
                    .WithMany(p => p.HistoricoInmueble)
                    .HasForeignKey(d => d.IdInmuebleCentroCoste)
                    .HasConstraintName("FK_historicoinmueble_InmuebleCentroCoste");

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.HistoricoInmueble)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historicoinmueble_provincia");

                entity.HasOne(d => d.IdTipoInmuebleNavigation)
                    .WithMany(p => p.HistoricoInmueble)
                    .HasForeignKey(d => d.IdTipoInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historicoinmueble_tipoInmueble");

                entity.HasOne(d => d.IdUsuarioNavigation)
                   .WithMany(p => p.HistoricoInmuebles)
                   .HasForeignKey(d => d.IdUsuario)
                   .HasConstraintName("FK_historicoinmueble_Usuario");

                entity.HasOne(d => d.IdTipoArticuloNavigation)
                  .WithMany(p => p.HistoricoInmueble)
                  .HasForeignKey(d => d.IdTipoArticulo)
                  .HasConstraintName("FK_historicoinmueble_TipoArticulo");
            });

            modelBuilder.Entity<HistoricoContratosClientes>(entity =>
            {
                entity.HasOne(d => d.IdContratoClienteNavigation)
                   .WithMany(p => p.HistoricoContratosClientes)
                   .HasForeignKey(d => d.IdContratoCliente)
                   .HasConstraintName("FK_HistoricoContratoCliente_ContratoCliente");

                entity.HasOne(d => d.IdConceptoFacturacionNavigation)
                    .WithMany(p => p.HistoricoContratosClientes)
                    .HasForeignKey(d => d.IdConceptoFacturacion)
                    .HasConstraintName("FK_HistoricoContratoCliente_Concepto");

                entity.HasOne(d => d.IdFormaPagoNavigation)
                    .WithMany(p => p.HistoricoContratosClientes)
                    .HasForeignKey(d => d.IdFormaPago)
                    .HasConstraintName("FK_HistoricoContratoCliente_FormaPago");

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.HistoricoContratosClientes)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoContratoCliente_Inmueble");

                //entity.HasOne(d => d.IdClienteNavigation)
                //   .WithMany(p => p.HistoricoContratosClientes)
                //   .HasForeignKey(d => d.IdCliente)
                //   .OnDelete(DeleteBehavior.ClientSetNull)
                //   .HasConstraintName("FK_HistoricoContratoCliente_Clientes");

                entity.HasOne(d => d.IdSwiftNavigation)
                    .WithMany(p => p.HistoricoContratosClientes)
                    .HasForeignKey(d => d.IdSwift)
                    .HasConstraintName("FK_HistoricoContratoCliente_Swift");

                entity.HasOne(d => d.IdTipoIpcNavigation)
                    .WithMany(p => p.HistoricoContratosClientes)
                    .HasForeignKey(d => d.IdTipoIpc)
                    .HasConstraintName("FK_HistoricoContratoCliente_TiposIPC");

                entity.HasOne(d => d.IdUsuarioNavigation)
                   .WithMany(p => p.HistoricoContratosClientes)
                   .HasForeignKey(d => d.IdUsuario)
                   .HasConstraintName("FK_HistoricoContratoCliente_Usuario");
            });

            modelBuilder.Entity<HistoricoContratosProveedores>(entity =>
            {
                entity.HasOne(d => d.IdContratoProveedorNavigation)
                   .WithMany(p => p.HistoricoContratosProveedores)
                   .HasForeignKey(d => d.IdContratoProveedor)
                   .HasConstraintName("FK_HistoricoContratoProveedor_ContratoProveedor");
                
                entity.HasOne(d => d.IdInmuebleNavigation)
                  .WithMany(p => p.HistoricoContratosProveedores)
                  .HasForeignKey(d => d.IdInmueble)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_HistoricoContratoProveedor_inmueble");

                entity.HasOne(d => d.IdInmuebleCentroCosteNavigation)
                    .WithMany(p => p.HistoricoContratosProveedores)
                    .HasForeignKey(d => d.IdInmuebleCentroCoste)
                    .HasConstraintName("FK_HistoricoContratoProveedor_CentroCoste");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.HistoricoContratosProveedores)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_HistoricoContratoProveedor_Usuario");

                entity.HasOne(d => d.IdPeriodicidadCosteNavigation)
                   .WithMany(p => p.HistoricoContratosProveedoresCoste)
                   .HasForeignKey(d => d.IdPeriodicidadCoste)
                   .HasConstraintName("FK_HistoricoContratoProveedor_PeriodicidadCoste");

                entity.HasOne(d => d.IdPeriodicidadRevisionNavigation)
                  .WithMany(p => p.HistoricoContratosProveedoresRevision)
                  .HasForeignKey(d => d.IdPeriodicidadRevision)
                  .HasConstraintName("FK_HistoricoContratoProveedor_PeriodicidadRevision");

                entity.HasOne(d => d.IdUsuarioNavigation)
                   .WithMany(p => p.HistoricoContratosProveedores)
                   .HasForeignKey(d => d.IdUsuario)
                   .HasConstraintName("FK_HistoricoContratoProveedor_Usuario");
            });

            modelBuilder.Entity<HistoricoArticulos>(entity =>
            {
                entity.Property(e => e.Articulo).IsUnicode(false);

                entity.Property(e => e.Estado).IsUnicode(false);

                entity.Property(e => e.HistorialObra).IsUnicode(false);

                entity.Property(e => e.Planos).IsUnicode(false);

                entity.HasOne(d => d.IdArticuloNavigation)
                  .WithMany(p => p.HistoricoArticulo)
                  .HasForeignKey(d => d.IdArticulo)
                  .HasConstraintName("FK_HistoricoArticulos_Articulo");

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.HistoricoArticulo)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoArticulos_Inmueble");

                entity.HasOne(d => d.IdPlantaNavigation)
                    .WithMany(p => p.HistoricoArticulo)
                    .HasForeignKey(d => d.IdPlanta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoArticulos_Planta");

                entity.HasOne(d => d.IdTipoArticuloNavigation)
                    .WithMany(p => p.HistoricoArticulo)
                    .HasForeignKey(d => d.IdTipoArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoArticulos_TipoArt");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.HistoricoArticulo)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_HistoricoArticulos_Usuario");
            });

            modelBuilder.Entity<Facturacion>(entity =>
            {
                entity.Property(e => e.CodigoAgrupacion).IsUnicode(false);
               
                entity.HasOne(d => d.IdConceptoFacturacionNavigation)
                    .WithMany(p => p.Facturacion)
                    .HasForeignKey(d => d.IdConceptoFacturacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_facturacion_conceptoFacturacion");

                entity.HasOne(d => d.IdConceptoFacturacionImprimirNavigation)
                    .WithMany(p => p.FacturacionImprimir)
                    .HasForeignKey(d => d.IdConceptoFacturacionImprimir)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_facturacion_conceptoFacturacionImprimir");

                entity.HasOne(d => d.IdContratoClienteNavigation)
                    .WithMany(p => p.Facturacion)
                    .HasForeignKey(d => d.IdContratoCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_facturacion_contratocliente");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Facturacion)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Facturacion_Usu");

                entity.HasOne(d => d.IdModalidadFacturaNavigation)
                   .WithMany(p => p.Facturacion)
                   .HasForeignKey(d => d.IdModalidadFactura)
                   .HasConstraintName("FK_Facturacion_ModalidadFactura");
            });

            modelBuilder.Entity<FormasDePago>(entity =>
            {
                entity.Property(e => e.FormaPago).IsUnicode(false);

                entity.Property(e => e.OtroDato).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.FormasDePago)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_FormasDePago_Usuario");
            });

            modelBuilder.Entity<Gastos>(entity =>
            {
                entity.Property(e => e.Cuentacontable).IsUnicode(false);

                entity.Property(e => e.Gasto).IsUnicode(false);

                entity.Property(e => e.Grupo).IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_Gastos_Empresa");

                entity.HasOne(d => d.IdTipoGastoNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.IdTipoGasto)
                    .HasConstraintName("FK_Gastos_TipoGasto");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Gastos_Usu");
            });

            modelBuilder.Entity<GastosAnuales>(entity =>
            {
                entity.HasOne(d => d.CodigoNavigation)
                    .WithMany(p => p.GastosAnuales)
                    .HasForeignKey(d => d.Codigo)
                    .HasConstraintName("FK_GastosAnuales_Inmuebles");
            });

            modelBuilder.Entity<Grupos>(entity =>
            {
                entity.Property(e => e.NombreGrupo).IsUnicode(false);
            });

            modelBuilder.Entity<HistorialObra>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.DireccionFacultativa).IsUnicode(false);

                entity.Property(e => e.NumeroExpediente).IsUnicode(false);

                entity.HasOne(d => d.IdContratoProveedorNavigation)
                    .WithMany(p => p.HistorialObra)
                    .HasForeignKey(d => d.IdContratoProveedor)
                    .HasConstraintName("FK_HistorialObra_ContratoProveedor");

                entity.HasOne(d => d.IdTipoFicheroNavigation)
                    .WithMany(p => p.HistorialObra)
                    .HasForeignKey(d => d.IdTipoFichero)
                    .HasConstraintName("FK_HistorialObra_TipoFichero");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.HistorialObra)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_HistorialObra_Usuario");

                entity.HasOne(d => d.IdTipoObraNavigation)
                   .WithMany(p => p.HistorialObra)
                   .HasForeignKey(d => d.IdTipoObra)
                   .HasConstraintName("FK_HistorialObra_TipoObra");
            });

            modelBuilder.Entity<HistoricoConceptoFacturacion>(entity =>
            {
                entity.Property(e => e.CodigoAgrupacion).IsUnicode(false);

                entity.Property(e => e.ConceptoFacturacion).IsUnicode(false);

                entity.HasOne(d => d.IdCatalogarConceptoNavigation)
                    .WithMany(p => p.HistoricoConceptoFacturacion)
                    .HasForeignKey(d => d.IdCatalogarConcepto)
                    .HasConstraintName("FK_HistoricoConceptoFacturacion_CatalogarConcepto");

                entity.HasOne(d => d.IdConceptoFacturacionNavigation)
                    .WithMany(p => p.HistoricoConceptoFacturacion)
                    .HasForeignKey(d => d.IdConceptoFacturacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoConceptoFacturacion_ConceptoFacturacion");

                entity.HasOne(d => d.IdHistoricoFacturacionNavigation)
                    .WithMany(p => p.HistoricoConceptoFacturacion)
                    .HasForeignKey(d => d.IdHistoricoFacturacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoConceptoFacturacion_HistoricoFacturacion");

                entity.HasOne(d => d.IdTipoConceptoNavigation)
                    .WithMany(p => p.HistoricoConceptoFacturacion)
                    .HasForeignKey(d => d.IdTipoConcepto)
                    .HasConstraintName("FK_HistoricoConceptoFacturacion_TipoConcepto");
            });

            modelBuilder.Entity<HistoricoFacturacion>(entity =>
            {
                entity.Property(e => e.Cliente).IsUnicode(false);

                entity.Property(e => e.CodigoPostal).IsUnicode(false);

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.FormaPago).IsUnicode(false);

                entity.Property(e => e.Municipio).IsUnicode(false);

                entity.Property(e => e.Nif).IsUnicode(false);

                entity.Property(e => e.Provincia).IsUnicode(false);

                //entity.HasOne(d => d.IdClienteNavigation)
                //    .WithMany(p => p.HistoricoFacturacion)
                //    .HasForeignKey(d => d.IdCliente)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_HistoricoFacturacion_Cliente");

                entity.HasOne(d => d.IdContratoClienteNavigation)
                    .WithMany(p => p.HistoricoFacturacion)
                    .HasForeignKey(d => d.IdContratoCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoFacturacion_ContratoCliente");

                entity.HasOne(d => d.IdDatoImprimirNavigation)
                    .WithMany(p => p.HistoricoFacturacion)
                    .HasForeignKey(d => d.IdDatoImprimir)
                    .HasConstraintName("FK_HistoricoFacturacion_DatoImprimir");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.HistoricoFacturacion)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoFacturacion_Empresa");

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.HistoricoFacturacion)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoFacturacion_Inmueble");
            });

            modelBuilder.Entity<HistoricoFacturacionBanco>(entity =>
            {
                entity.Property(e => e.Iban).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Swift).IsUnicode(false);

                entity.HasOne(d => d.IdContratoclienteNavigation)
                    .WithMany(p => p.HistoricoFacturacionBanco)
                    .HasForeignKey(d => d.IdContratocliente)
                    .HasConstraintName("FK_HistoricoFacturacionBanco_ContratoCliente");

                entity.HasOne(d => d.IdHistoricoFacturacionNavigation)
                    .WithMany(p => p.HistoricoFacturacionBanco)
                    .HasForeignKey(d => d.IdHistoricoFacturacion)
                    .HasConstraintName("FK_HistoricoFacturacionBanco_HistoricoFacturacion");
            });

            modelBuilder.Entity<HistoricoFacturacionSuperficie>(entity =>
            {
                entity.HasOne(d => d.IdHistoricoFacturacionNavigation)
                    .WithMany(p => p.HistoricoFacturacionSuperficie)
                    .HasForeignKey(d => d.IdHistoricoFacturacion)
                    .HasConstraintName("FK_HistoricoFacturacionSuperficie_HistoricoFacturacion");

                entity.HasOne(d => d.IdTipoArticuloNavigation)
                    .WithMany(p => p.HistoricoFacturacionSuperficie)
                    .HasForeignKey(d => d.IdTipoArticulo)
                    .HasConstraintName("FK_HistoricoFacturacionSuperficie_TipoArticulo");
            });

            modelBuilder.Entity<HistoricoSeguros>(entity =>
            {
                entity.Property(e => e.ArchivoDigital).IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.HistoricoSeguros)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoricoSeguros_Empresa");

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.HistoricoSeguros)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historicoSeguro_inmueble");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.HistoricoSeguros)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_HistoricoSeguros_Usuario");
            });

            modelBuilder.Entity<HistoricoTasaciones>(entity =>
            {
                entity.Property(e => e.ArchivoDigital).IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.HistoricoTasaciones)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historicoTasacion_empresa");

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.HistoricoTasaciones)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historicoTasacion_inmueble");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.HistoricoTasaciones)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_HistoricoTasaciones_Usuario");
            });

            modelBuilder.Entity<Incidencias>(entity =>
            {
                entity.Property(e => e.Afecta).IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.DescripcionResolucion).IsUnicode(false);

                entity.Property(e => e.Incidencia).IsUnicode(false);

                entity.Property(e => e.Lpd).IsUnicode(false);

                entity.Property(e => e.Servicio).IsUnicode(false);

                entity.HasOne(d => d.IdTipoFicheroNavigation)
                    .WithMany(p => p.Incidencias)
                    .HasForeignKey(d => d.IdTipoFichero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Incidencias_TipoFichero");

                entity.HasOne(d => d.IdContratoProveedorNavigation)
                    .WithMany(p => p.Incidencias)
                    .HasForeignKey(d => d.IdContratoProveedor)
                    .HasConstraintName("FK_Incidencias_ContratoProveedor");


                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Incidencias)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Incidencias_Usuario");
            });

            modelBuilder.Entity<InmuebleCentroCoste>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<Inmuebles>(entity =>
            {
                entity.Property(e => e.Abreviatura).IsUnicode(false);

                entity.Property(e => e.ArchivoDigital).IsUnicode(false);

                entity.Property(e => e.Calle).IsUnicode(false);

                entity.Property(e => e.CodigoPostal).IsUnicode(false);

                entity.Property(e => e.HistorialIncidencia).IsUnicode(false);

                entity.Property(e => e.Inmueble).IsUnicode(false);

                entity.Property(e => e.Municipio).IsUnicode(false);

                entity.Property(e => e.ReferenciaCatastralGeneral).IsUnicode(false);

                entity.HasOne(d => d.IdConceptoFacturacionNavigation)
                    .WithMany(p => p.Inmuebles)
                    .HasForeignKey(d => d.IdConceptoFacturacion)
                    .HasConstraintName("FK_Inmuebles_Concepto");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Inmuebles)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_inmueble_empresa");

                entity.HasOne(d => d.IdInmuebleCentroCosteNavigation)
                    .WithMany(p => p.Inmuebles)
                    .HasForeignKey(d => d.IdInmuebleCentroCoste)
                    .HasConstraintName("FK_Inmuebles_InmuebleCentroCoste");

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Inmuebles)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_inmueble_provincia");

                entity.HasOne(d => d.IdTipoInmuebleNavigation)
                    .WithMany(p => p.Inmuebles)
                    .HasForeignKey(d => d.IdTipoInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_inmueble_tipoInmueble");

                entity.HasOne(d => d.IdTipoArticuloNavigation)
                   .WithMany(p => p.Inmuebles)
                   .HasForeignKey(d => d.IdTipoArticulo)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_inmueble_tipoArticulo");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Inmuebles)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Inmuebles_Usu");
            });

            modelBuilder.Entity<Licencias>(entity =>
            {
                entity.HasKey(e => e.IdLicencia)
                    .HasName("PK_InmuebleLicencias");

                entity.Property(e => e.ArchivoDigital).IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.NumeroExpediente).IsUnicode(false);

                entity.Property(e => e.Organismo).IsUnicode(false);

                entity.HasOne(d => d.IdTipoFicheroNavigation)
                    .WithMany(p => p.Licencias)
                    .HasForeignKey(d => d.IdTipoFichero)
                    .HasConstraintName("FK_Licencias_TipoFichero");

                entity.HasOne(d => d.IdTipoLicenciaNavigation)
                    .WithMany(p => p.Licencias)
                    .HasForeignKey(d => d.IdTipoLicencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InmuebleLicencias_TipoLicencia");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Licencias)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Licencias_Usuario");
            });

            modelBuilder.Entity<Mantenimientos>(entity =>
            {
                entity.Property(e => e.Servicio).IsUnicode(false);

                entity.HasOne(d => d.IdContratoProveedorNavigation)
                    .WithMany(p => p.Mantenimientos)
                    .HasForeignKey(d => d.IdContratoProveedor)
                    .HasConstraintName("FK_Mantenimientos_ContratoProveedor");

                entity.HasOne(d => d.IdNormaNavigation)
                    .WithMany(p => p.Mantenimientos)
                    .HasForeignKey(d => d.IdNorma)
                    .HasConstraintName("FK_Mantenimientos_Normas");

                entity.HasOne(d => d.IdPeriodicidadServicioNavigation)
                    .WithMany(p => p.Mantenimientos)
                    .HasForeignKey(d => d.IdPeriodicidadServicio)
                    .HasConstraintName("FK_Mantenimientos_PeriodicidadServicio");


                entity.HasOne(d => d.IdTipoFicheroNavigation)
                    .WithMany(p => p.Mantenimientos)
                    .HasForeignKey(d => d.IdTipoFichero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mantenimientos_TipoFichero");

                entity.HasOne(d => d.IdTipoInstalacionNavigation)
                    .WithMany(p => p.Mantenimientos)
                    .HasForeignKey(d => d.IdTipoInstalacion)
                    .HasConstraintName("FK_Mantenimientos_TipoInstalacion");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Mantenimientos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Mantenimientos_Usuarios");

                entity.HasOne(d => d.IdTipoMantenimientoNavigation)
                  .WithMany(p => p.Mantenimientos)
                  .HasForeignKey(d => d.IdTipoMantenimiento)
                  .HasConstraintName("FK_Mantenimientos_TipoMantenimiento");
            });

            modelBuilder.Entity<Normas>(entity =>
            {
                entity.Property(e => e.ArchivoDigital).IsUnicode(false);

                entity.Property(e => e.TextoDescriptivo).IsUnicode(false);

                entity.HasOne(d => d.IdTipoInstalacionNavigation)
                    .WithMany(p => p.Normas)
                    .HasForeignKey(d => d.IdTipoInstalacion)
                    .HasConstraintName("FK_Normas_TipoInstalacion");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Normas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Normas_Usuario");

                entity.HasOne(d => d.IdTipoOrigenNavigation)
                   .WithMany(p => p.Normas)
                   .HasForeignKey(d => d.IdTipoOrigen)
                   .HasConstraintName("FK_Normas_TipoOrigen");
            });

            modelBuilder.Entity<ObrasFichero>(entity =>
            {
                entity.HasOne(d => d.IdHistorialObraNavigation)
                    .WithMany(p => p.ObrasFichero)
                    .HasForeignKey(d => d.IdHistorialObra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObrasFichero_Obras");

                entity.HasOne(d => d.IdTipoFicheroNavigation)
                    .WithMany(p => p.ObrasFichero)
                    .HasForeignKey(d => d.IdTipoFichero)
                    .HasConstraintName("FK_ObrasFichero_TipoFichero");
            });

            modelBuilder.Entity<PeriodicidadServicio>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<Periodicidad>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<Provincias>(entity =>
            {
                entity.Property(e => e.NumeroConcierto).IsUnicode(false);

                entity.Property(e => e.Provincia).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Provincias)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Provincias_Usu");
            });

            modelBuilder.Entity<ReferenciasCatastrales>(entity =>
            {
                entity.Property(e => e.DescripcionFinca).IsUnicode(false);

                entity.Property(e => e.ReferenciaCatastral).IsUnicode(false);

                entity.Property(e => e.TipoPago).IsUnicode(false);

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.ReferenciasCatastrales)
                    .HasForeignKey(d => d.IdInmueble)
                    .HasConstraintName("FK_ReferenciasCatastrales_Inmueble");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ReferenciasCatastrales)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_ReferenciasCatastrales_Usu");
            });

            modelBuilder.Entity<SuperficieTasacion>(entity =>
            {
                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.SuperficieTasacion)
                    .HasForeignKey(d => d.IdArticulo)
                    .HasConstraintName("FK_SuperficieTasacion_Articulo");

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.SuperficieTasacion)
                    .HasForeignKey(d => d.IdInmueble)
                    .HasConstraintName("FK_SuperficieTasacion_Inmueble");

                entity.HasOne(d => d.IdTasacionNavigation)
                    .WithMany(p => p.SuperficieTasacion)
                    .HasForeignKey(d => d.IdTasacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuperficieTasacion_Tasacion");
            });

            modelBuilder.Entity<Swift>(entity =>
            {
                entity.HasKey(e => e.IdSwift)
                    .HasName("PK_Switch");

                entity.Property(e => e.Banco).IsUnicode(false);

                entity.Property(e => e.Swift1).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Swift)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Swift_Usuarios");
            });

            modelBuilder.Entity<TareaPeriodica>(entity =>
            {
                
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Estado).IsUnicode(false);

                entity.Property(e => e.Propietario).IsUnicode(false);

                entity.Property(e => e.TipoRecurrente).IsUnicode(false);

                entity.Property(e => e.Titulo).IsUnicode(false);

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.TareaPeriodica)
                    .HasForeignKey(d => d.IdTarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tareaPeriodica_tarea");

                entity.HasOne(d => d.IdTipoFicheroNavigation)
                    .WithMany(p => p.TareaPeriodica)
                    .HasForeignKey(d => d.IdTipoFichero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TareaPeriodica_TipoFichero");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TareaPeriodica)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_TareaPeriodica_Usuario");

                entity.HasOne(d => d.IdUsuarioAsignadoNavigation)
                   .WithMany(p => p.TareaPeriodicaAsignada)
                   .HasForeignKey(d => d.AsignadaA)
                   .HasConstraintName("FK_TareaPeriodica_UsuarioAsignado");

                entity.HasOne(d => d.IdUsuarioResponsableNavigation)
                   .WithMany(p => p.TareaPeriodicaResponsable)
                   .HasForeignKey(d => d.IdResponsable)
                   .HasConstraintName("FK_TareaPeriodica_UsuarioResponsable");

                entity.HasOne(d => d.IdPeriodicidadNavigation)
                  .WithMany(p => p.TareaPeriodica)
                  .HasForeignKey(d => d.IdPeriodicidad)
                  .HasConstraintName("FK_TareaPeriodica_Periodicidad");
            });

            modelBuilder.Entity<Tareas>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Tarea).IsUnicode(false);

                entity.HasOne(d => d.IdTipoFicheroNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdTipoFichero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tareas_TipoFichero");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Tareas_Usu");
            });

            modelBuilder.Entity<TipoArticulos>(entity =>
            {
                entity.HasKey(e => e.IdTipoArticulo)
                    .HasName("PK_TiposArticulos");

                entity.Property(e => e.Tipoarticulo).IsUnicode(false);

                entity.HasOne(d => d.IdClasificacionNavigation)
                    .WithMany(p => p.TipoArticulos)
                    .HasForeignKey(d => d.IdClasificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TiposArticulos_Clasificacion");

                entity.HasOne(d => d.IdTipoConceptoArticuloNavigation)
                    .WithMany(p => p.TipoArticulos)
                    .HasForeignKey(d => d.IdTipoConceptoArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TiposArticulos_TipoConceptoArticulo");

                entity.HasOne(d => d.IdTipoMedidaNavigation)
                    .WithMany(p => p.TipoArticulos)
                    .HasForeignKey(d => d.IdTipoMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TiposArticulos_TipoMedida");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TipoArticulos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_TipoArticulos_Usu");
            });

            modelBuilder.Entity<HistorialOcupacion>(entity =>
            {
                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.HistorialOcupacion)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_historialOcupacion_articulo");

                entity.HasOne(d => d.IdContratoClienteNavigation)
                   .WithMany(p => p.HistorialOcupacion)
                   .HasForeignKey(d => d.IdContratoCliente)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_historialOcupacion_contratoCliente");

               
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.HistorialOcupacion)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_HistorialOcupacion_Usuario");
            });


            modelBuilder.Entity<TipoConcepto>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<TipoConceptoArticulo>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<TipoFichero>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<TipoGasto>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<TipoInmuebles>(entity =>
            {
                entity.HasKey(e => e.IdTipoInmueble)
                    .HasName("PK_TiposInmuebles");

                entity.Property(e => e.TipoInmueble).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TipoInmuebles)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_TipoInmuebles_Usu");
            });

            modelBuilder.Entity<TipoInstalacion>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TipoInstalacion)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_TipoInstalacion_Usu");
            });

            modelBuilder.Entity<TipoIpc>(entity =>
            {
                entity.HasKey(e => e.IdTipoIpc)
                    .HasName("PK_TiposIPC");

                entity.Property(e => e.Ipc).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TipoIpc)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_TipoIPC_Usuario");
            });

            modelBuilder.Entity<TipoIva>(entity =>
            {
                entity.HasKey(e => e.IdTipoIva)
                    .HasName("PK_TiposIVA");

                entity.Property(e => e.CuentaContable).IsUnicode(false);

                entity.Property(e => e.TipoIva1).IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.TipoIva)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK_TiposIVA_Empresas");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TipoIva)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_TipoIVA_Usuario");
            });

            modelBuilder.Entity<TipoLicencia>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TipoLicencia)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_TipoLicencia_Usu");
            });

            modelBuilder.Entity<TipoMantenimiento>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<TipoMedida>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<TipoPlanta>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<TipoOrigen>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<TipoObra>(entity =>
            {
                entity.Property(e => e.Valor).IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioInmueble>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioInmueble)
                    .HasName("PK_UsuarioInmueble");

                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioInmueble_Inmueble");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioInmueble_Usu");
            });

            modelBuilder.Entity<ContratoClienteConceptoFactura>(entity =>
            {
    
                entity.HasOne(d => d.IdContratoClienteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdContratoCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContratoClienteConceptoFactura_ContratosClientes");

                entity.HasOne(d => d.IdConceptoFacturacionNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdConceptoFacturacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContratoClienteConceptoFactura_ConceptoFacturacion");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.NombreUsuario).IsUnicode(false);
            });

            modelBuilder.Entity<VentaParcialInmueble>(entity =>
            {
               
                entity.HasOne(d => d.IdInmuebleNavigation)
                    .WithMany(p => p.VentaParcialInmuebles)
                    .HasForeignKey(d => d.IdInmueble)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VentaParcialInmueble_Inmueble");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
  
    }
    public class DbSchemaAwareModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context)
            => context is CFADbContext myContext ?
            (context.GetType(), myContext.Schema) :
            (object)context.GetType();
    }
    public static class DbContextExtensions
    {
        public static T ApplyValues<T>(this DbSet<T> set, T entity, params object[] key) where T : class
        {
            var dbEntity = set.Find(key);
            if (dbEntity == null)
            {
                return null;
            }

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !(p.PropertyType.IsInterface && p.PropertyType.IsGenericType &&
                              p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                             && p.CanWrite).ToList();
            properties.ForEach(p =>
            {
                p.GetValue(dbEntity);
                p.SetValue(dbEntity, p.GetValue(entity));
            });

            return dbEntity;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
