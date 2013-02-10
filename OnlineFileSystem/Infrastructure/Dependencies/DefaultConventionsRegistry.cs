using OnlineFileSystem.Helpers;
using OnlineFileSystem.Models;
using StructureMap.Configuration.DSL;

namespace OnlineFileSystem.Infrastructure.Dependencies
{
    public class DefaultConventionsRegistry : Registry
    {
        public DefaultConventionsRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssembliesFromApplicationBaseDirectory(
                    assembly => assembly.FullName.Contains("OnlineFileSystem"));
                scan.WithDefaultConventions();
            });
            For(typeof(IConfiguration<>))
                              .Use(typeof(AppConfigConfiguration<>));
            For<IDiObject>().Use<DiObject>().Ctor<string>("basepath").EqualToAppSetting("AppSettings.RootPath");
            //  .Use<YourpayWsCreditCardProcessor>();
        }
    }
}