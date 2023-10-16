using Core;
using Game;
using Game.Core;
using Game.LevelGenerator;
using GameplaySystem;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BaseSystem
{
    public class BasesCounterController : MonoBehaviour
    {
        [Inject] private BasesController _basesController;
        [Inject] private ObjectPoolModule _poolModule;
        [Inject] private SaveModule _saveModule;

        [SerializeField] private GameData _gameData;
        [SerializeField] private BaseCounterView _baseCounterPrefab;

        private List<BaseCounter> _counters = new List<BaseCounter>();

        private void Start()
        {
            var bases = _basesController.GetBases(Squad.Player);
            var counterView = _poolModule.Spawn(_baseCounterPrefab.gameObject, transform.localPosition, Quaternion.identity, transform).GetComponent<BaseCounterView>();
            counterView.InitView(_gameData.BasesPlayer.ColorSquad);
            counterView.name = Squad.Player.ToString();
            var counter = new BaseCounter(Squad.Player, bases.Count, counterView);
            _counters.Add(counter);

            var save = _saveModule.Load<LevelSettings>(GameConstants.LevelSettingsKey);
            
            for (int i = 0; i < save.CountEnemys; i++)
            {
                var enemy = _gameData.Enemys[i];
                bases = _basesController.GetBases(enemy.CurrentSquad);
                counterView = _poolModule.Spawn(_baseCounterPrefab.gameObject, transform.localPosition, Quaternion.identity, transform).GetComponent<BaseCounterView>();
                counterView.InitView(enemy.ColorSquad);
                counterView.name = enemy.CurrentSquad.ToString();
                counter = new BaseCounter(enemy.CurrentSquad, bases.Count, counterView);
                _counters.Add(counter);
            }

            _basesController.OnChangeBaseSquad += ChangeBaseSquad;
        }

        private void OnDestroy()
        {
            _basesController.OnChangeBaseSquad -= ChangeBaseSquad;
        }

        private void ChangeBaseSquad(BaseView b, Squad s)
        {
            var oldSquad = _counters.Find(x => x.CurrentSquad == b.GetSquad());
            if (oldSquad != null)
            {
                oldSquad.Count--;
                oldSquad.ChangeCount();
            }

            var newSquad = _counters.Find(x => x.CurrentSquad == s);
            if (newSquad != null)
            {
                newSquad.Count++;
                newSquad.ChangeCount();
            }
        }
    }

    public class BaseCounter
    {
        public Squad CurrentSquad;
        public int Count;
        public BaseCounterView View;

        public BaseCounter(Squad squad, int count, BaseCounterView view)
        {
            CurrentSquad = squad;
            Count = count;
            View = view;
            ChangeCount();
        }

        public void ChangeCount() => View.ChangeCount(Count);
    }
}
