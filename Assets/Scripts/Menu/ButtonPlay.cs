using Core;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MenuSystem
{
    public class ButtonPlay : MonoBehaviour
    {
        [Inject] private SceneLoader _sceneLoader;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => OnButtonClick());
        }

        public async Task OnButtonClick()
        {
            await _sceneLoader.LoadNextScene();
        }
    }
}
