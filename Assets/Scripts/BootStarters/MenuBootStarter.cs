using Core.UI;
using Game.Configs;
using GameplaySystem.UI;
using VContainer;
using VContainer.Unity;

namespace MenuSystem
{
    public class MenuBootStarter : BootStarter
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<MenuController>(Lifetime.Scoped);
            builder.Register<ConfigsLoader>(Lifetime.Scoped);

            builder.Register<UIController>(Lifetime.Scoped);
            builder.Register<LevelGenerator>(Lifetime.Scoped).As<LevelGenerator, IStartable>();

        }
    }
}