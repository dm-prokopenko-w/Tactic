using Core;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using AISystem;
using BaseSystem;

namespace GameplaySystem
{
    public class GameplayBootStarter : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameplayManager>(Lifetime.Scoped);
            builder.Register<BasesController>(Lifetime.Scoped).As<BasesController, ITickable>();
            builder.Register<UnitsManager>(Lifetime.Scoped);
            builder.Register<AIController>(Lifetime.Scoped);

            var moduls = FindObjectsOfType<Moduls>();
            foreach (var modul in moduls)
            {
                modul.Register(builder);
            }
    }
}
}