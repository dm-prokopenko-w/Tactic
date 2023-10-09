using VContainer;
using VContainer.Unity;
using AISystem;
using BaseSystem;
using Core;
using Core.ControlSystem;
using Game.Configs;

namespace GameplaySystem
{
    public class GameplayBootStarter : BootStarter
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<ObjectPoolModule>(Lifetime.Scoped);
            builder.Register<ControlModule>(Lifetime.Scoped);
            builder.Register<ConfigsLoader>(Lifetime.Scoped);
            builder.Register<AIModule>(Lifetime.Scoped).As<AIModule, ITickable>();
            builder.Register<GameplayManager>(Lifetime.Scoped);
            builder.Register<BasesController>(Lifetime.Scoped).As<BasesController, ITickable>();
            builder.Register<UnitsManager>(Lifetime.Scoped);
            builder.Register<AIController>(Lifetime.Scoped);
            builder.Register<CameraController>(Lifetime.Scoped);
        }
    }
}