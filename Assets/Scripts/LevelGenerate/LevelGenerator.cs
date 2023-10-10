using Core.UI;
using Game;
using VContainer;
using VContainer.Unity;
using Game.Configs;
using System.Threading.Tasks;
using Game.Core;

namespace GameplaySystem.UI
{
    public class LevelGenerator : IStartable
    {
        [Inject] private UIController _uiController;
        [Inject] private ConfigsLoader _configsLoader;
        [Inject] private SaveModule _saveLoader;

        private const string LevelSettingsKey = "LevelSettings";

        private LevelSettings _currentSave = new LevelSettings();

        public void Start()
        {
            _ = Init();
        }

        private async Task Init()
        {
            GameData data = await _configsLoader.LoadConfig(GameConstants.GameData) as GameData;
            
            InitChangeCountEnemys(data.Enemys.Count);
            InitSaveGenereteBtn();
        }

        private void InitChangeCountEnemys(int countEnemy)
        {
            var drop = _uiController.GetDropdownById(UIConstants.SettingsCountEnemy);
            if (drop == null) return;
            new ChangeCountEnemys().Init(_currentSave, countEnemy, drop);
        }

        private void InitSaveGenereteBtn()
        {
            var btn = _uiController.GetButtonById(UIConstants.SettingsSaveGenerete);
            if (btn == null) return;
            _saveLoader.Save(LevelSettingsKey, _currentSave);
        }
    }

    public class LevelSettings
    {
        public int CountEnemys;
    }
}