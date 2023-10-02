using VContainer;

namespace MenuSystem
{
    public class MenuBootStarter : BootStarter
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<MenuController>(Lifetime.Scoped);
        }
    }
}