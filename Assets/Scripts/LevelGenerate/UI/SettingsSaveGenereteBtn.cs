using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.LevelGenerator
{
    public class SettingsSaveGenereteBtn : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private Button _button;

        [Inject]
        public void Construct()
        {
            _uiController.AddUIItem(new UIItem(UIConstants.SettingsSaveGenerete, _button));
        }

        private void OnDestroy()
        {
            _uiController.RemoveUIItem(UIConstants.SettingsSaveGenerete);
        }
    }
}