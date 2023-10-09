using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core.PopupsSystem
{
    public class PopupPreloader : Popup
    {
        [Inject] private SceneLoader _sceneLoader;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _progressBar;

        public override void Init() 
        {
            _sceneLoader.OnUpdateProgressScene += UpdateProgressBar;
        }

        private void UpdateProgressBar(float progress)
        {
            Debug.Log($"{progress}");
            _progressBar.fillAmount = progress;
            int procent = (int)progress * 100;
            _text.text = "Loading..." + procent;
        }

        private void OnDestroy()
        {
            _sceneLoader.OnUpdateProgressScene -= UpdateProgressBar;
        }
    }
}
