using Core.UI;
using Game;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GameplaySystem.UI
{
    public class SettingsSaveGenereteBtn : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private Button _button;

        [Inject]
        public void Construct()
        {
            _uiController.AddUIItem(new UIItem(UIConstants.SettingsCountEnemy, _button));
        }

        private void OnDestroy()
        {
            _uiController.RemoveUIItem(UIConstants.SettingsCountEnemy);
        }
    }
}