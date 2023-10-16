using Core.UI;
using VContainer;
using VContainer.Unity;
using System.Threading.Tasks;
using Game.Core;
using GameplaySystem;

namespace Game.LevelGenerator
{
    public class LevelGenerator : IStartable
    {
        [Inject] private UIController _uiController;
        [Inject] private AssetLoader _assetLoader;
        [Inject] private SaveModule _saveModule;

        private LevelSettings _currentSave;
        private int _countEnemy;

        public void Start()
        {
            _ = InitCountEnemys();
            _currentSave = _saveModule.Load<LevelSettings>(GameConstants.LevelSettingsKey);

            if (_currentSave == null)
            {
                _currentSave = new LevelSettings();
            }
        }

        private async Task InitCountEnemys()
        {
            GameData data = await _assetLoader.LoadConfig(GameConstants.GameData) as GameData;
            _countEnemy = data.Enemys.Count;
        }

        public void Init()
        {
            InitChangeCountEnemys(_countEnemy);
            _uiController.SetFuncById(UIConstants.SettingsSaveGenerete, SaveGenerete);
        }

        private void InitChangeCountEnemys(int countEnemy)
        {
            var drop = _uiController.GetDropdownById(UIConstants.SettingsCountEnemy);
            if (drop == null) return;
            new ChangeCountEnemys().Init(_currentSave, countEnemy, drop);
        }

        private void SaveGenerete()
        {
            _saveModule.Save(GameConstants.LevelSettingsKey, _currentSave);
        }
    }
}