using Core.PopupsSystem;
using VContainer;

namespace Game.LevelGenerator
{
    public class LevelGeneratePopup : Popup
    {
        [Inject] private PopupsModule _popupsModule;
        [Inject] private LevelGenerator _levelGenerator;

        private void Start()
        {
            _popupsModule.AddedBtnClosePopup();
            _levelGenerator.Init();
        }
    }
}