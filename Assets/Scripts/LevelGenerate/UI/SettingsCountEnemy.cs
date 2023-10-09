using Core.UI;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.UI
{
    public class SettingsCountEnemy : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private TMP_Dropdown _dropdown;

        private void Awake()
        {
            _uiController.AddDropdownItem(UIConstants.SettingsSaveGenerete, _dropdown);
        }
    }
}
