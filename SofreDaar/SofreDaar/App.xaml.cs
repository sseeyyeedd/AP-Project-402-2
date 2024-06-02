using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using SofreDaar.Infrastructure;

namespace SofreDaar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SofreDaar;Trusted_Connection=True;"));
           
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                //Uncomment the code below if you want to delete the database
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }

}
