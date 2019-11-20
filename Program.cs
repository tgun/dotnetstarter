using dotnetstarter.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace dotnetstarter {
    public class Program {
        public static void Main(string[] args) {
            IWebHost host = CreateWebHostBuilder(args).Build();

            // -- Sets up a database using Code-First, seeding with data if it has not yet been created.
            using (IServiceScope scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<AppDbContext>()) {
                context.Database.EnsureCreated();
            }

            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
