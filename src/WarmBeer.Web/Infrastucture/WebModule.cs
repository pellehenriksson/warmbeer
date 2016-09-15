using Autofac;
using WarmBeer.Core.Infrastructure.Persistence;

namespace WarmBeer.Web.Infrastucture
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WarmBeerDbContext>()
                .InstancePerLifetimeScope()
                .AsSelf();
        }
    }
}
