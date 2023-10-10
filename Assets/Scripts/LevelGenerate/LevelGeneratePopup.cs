using Core.PopupsSystem;
using VContainer;

namespace Game.Popups
{
    public class LevelGeneratePopup : Popup
    {
        [Inject] private PopupsModule _popupsModule;

        private void Start()
        {
            _popupsModule.AddedBtnClosePopup();
        }
    }
}