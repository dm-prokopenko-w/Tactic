using Core.UI;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.LevelGenerator
{
    public class SettingsCountEnemy : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private TMP_Dropdown _dropdown;

        [Inject]
        public void Construct()
        {
            _uiController.AddUIItem(new UIItem(UIConstants.SettingsCountEnemy, _dropdown));
        }

        private void OnDestroy()
        {
            _uiController.RemoveUIItem(UIConstants.SettingsCountEnemy);
        }
    }
}
