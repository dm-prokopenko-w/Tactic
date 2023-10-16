using TMPro;

namespace Game.LevelGenerator
{
    public class ChangeCountEnemys
    {
        private LevelSettings _save;

        public void Init(LevelSettings save, int countEnemy, TMP_Dropdown drop)
        {
            _save = save;
            drop.ClearOptions();
            drop.AddOptions(new System.Collections.Generic.List<TMP_Dropdown.OptionData>());

            var options = new System.Collections.Generic.List<TMP_Dropdown.OptionData>();

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

            if (_save.CountEnemys == 0)
            {
                ChangeCount(0);
                drop.value = 0;
            }
            else
            {
                drop.value = _save.CountEnemys - 1;
            }
        }

        private void ChangeCount(int countEnemys)
        {
            _save.CountEnemys = countEnemys + 1;
        }
    }
}
