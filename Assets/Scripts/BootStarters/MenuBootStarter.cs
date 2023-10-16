using Game;
using Game.LevelGenerator;
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
            builder.Register<AssetLoader>(Lifetime.Scoped);

            builder.Register<LevelGenerator>(Lifetime.Scoped).As<LevelGenerator, IStartable>();
        }
    }
}