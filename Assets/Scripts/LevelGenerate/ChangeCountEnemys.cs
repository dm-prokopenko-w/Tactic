using TMPro;

namespace GameplaySystem.UI
{
    public class ChangeCountEnemys
    {
        private LevelSettings _save;

        public void Init(LevelSettings save, int countEnemy, TMP_Dropdown drop)
        {
            _save = save;
            drop.ClearOptions();
            drop.AddOptions(new System.Collections.Generic.List<TMPro.TMP_Dropdown.OptionData>());

            var options = new System.Collections.Generic.List<TMPro.TMP_Dropdown.OptionData>();

            for (int i = 0; i < countEnemy; i++)
            {
                var opt = new TMP_Dropdown.OptionData()
                {
                    text = (i + 1).ToString()
                };

                options.Add(opt);
            }

            drop.AddOptions(options);
            drop.onValueChanged.AddListener(ChangeCount);
        }

        private void ChangeCount(int countEnemys)
        {
            _save.CountEnemys = countEnemys + 1;
        }
    }
}
