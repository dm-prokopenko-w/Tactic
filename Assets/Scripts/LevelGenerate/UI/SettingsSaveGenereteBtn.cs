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

        private void Awake()
        {
            _uiController.AddButtonItem(UIConstants.SettingsCountEnemy, _button);
        }
    }
}
