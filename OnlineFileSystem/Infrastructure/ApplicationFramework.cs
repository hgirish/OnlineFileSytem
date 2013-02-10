using System.Web.Mvc;
using OnlineFileSystem.Infrastructure.Dependencies;
using StructureMap;

namespace OnlineFileSystem.Infrastructure
{
    public static class ApplicationFramework
    {
        public static void Bootstrap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<DefaultConventionsRegistry>();
               
            }
            );

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }

        //public static void Start()
        //{
        //    var runAtStartups = ObjectFactory.GetAllInstances<IRunAtStartup>();

        //    foreach (var task in runAtStartups)
        //    {
        //        task.Execute();
        //    }
        //}
    }
}