using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using gg_test_fa;
using gg_test_fa.services;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace gg_test_fa
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IFileService, FileService>();
        }
    }
}