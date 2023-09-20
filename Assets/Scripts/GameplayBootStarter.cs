using Core;
using VContainer;
using VContainer.Unity;

namespace GameplaySystem
{
    public class GameplayBootStarter : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var moduls = FindObjectsOfType<Moduls>();
            foreach (var modul in moduls)
            {
                modul.Register(builder);
            }
        }
    }
}