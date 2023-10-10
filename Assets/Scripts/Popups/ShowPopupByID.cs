using Core;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Popups
{
    public class ShowPopupByID : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private PopupsID _id;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _uiController.AddUIItem(new UIItem(CoreConstants.ShowPopupByIdBtn, _button, _id.ToString()));
        }
    }
}