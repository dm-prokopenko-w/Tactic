using Core;
using Core.UI;
using VContainer;

namespace MenuSystem
{
    public class ButtonPlay : ButtonListener
    {
        [Inject] private SceneLoader _sceneLoader;

        protected override void OnButtonClick()
        {
            _sceneLoader.LoadNextScene();
        }
    }
}
