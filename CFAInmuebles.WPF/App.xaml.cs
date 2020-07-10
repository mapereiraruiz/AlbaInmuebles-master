using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using CFAInmuebles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading;
using CFAInmuebles.Domain.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CFAInmuebles.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public CFADbContext db;

        public List<CFADbContext> dbsALTAI;
        protected override void OnStartup(StartupEventArgs e)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var builder = new DbContextOptionsBuilder<CFADbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseLazyLoadingProxies();
            builder.UseSqlServer(connectionString);

            db = new CFADbContext(builder.Options);

            // Vamos a crear dinamicamente las cadenas de conexión para ALTAI. Una por cada empresa

            var empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();

            dbsALTAI = new List<CFADbContext>();

            foreach (var empresa in empresas)
            {
                var connectionStringALTAI = configuration.GetConnectionString("ALTAIConnection");
                connectionStringALTAI = connectionStringALTAI.Replace("[COD_EMPRESA]", "CFA_ALTAI");
                connectionStringALTAI = connectionStringALTAI.Replace("[COD_EMPRESA_USER]", empresa.EmpresaALTAI);
                
                builder.UseSqlServer(connectionStringALTAI);
                builder.UseLazyLoadingProxies();
                builder.ReplaceService<IModelCacheKeyFactory, DbSchemaAwareModelCacheKeyFactory>();
                dbsALTAI.Add(new CFADbContext(builder.Options, "CONT_" + empresa.EmpresaALTAI));
            }

            MainWindow app = new MainWindow();
            MainWindowVM context = new MainWindowVM();
            app.DataContext = context;
            app.Show();

            base.OnStartup(e);
        }

    }
}
