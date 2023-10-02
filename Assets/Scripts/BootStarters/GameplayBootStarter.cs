using VContainer;
using VContainer.Unity;
using AISystem;
using BaseSystem;

namespace GameplaySystem
{
    public class GameplayBootStarter : BootStarter
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<GameplayManager>(Lifetime.Scoped);
            builder.Register<BasesController>(Lifetime.Scoped).As<BasesController, ITickable>();
            builder.Register<UnitsManager>(Lifetime.Scoped);
            builder.Register<AIController>(Lifetime.Scoped);
            builder.Register<CameraController>(Lifetime.Scoped);
        }
    }
}