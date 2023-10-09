using BaseSystem;
using Core;
using GameplaySystem;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace AISystem
{
    public class AIController
    {
        [Inject] private AIModule _aiModule;
        [Inject] private BasesController _basesController;
        [Inject] private UnitsManager _unitsManager;

        private List<AIEmptyItem> _enemys = new List<AIEmptyItem>();

        public void Init(List<ItemData> enemyData)
        {
            foreach (var enemy in enemyData)
            {
                var item = new AIItem()
                {
                    BasesData = new List<BaseData>(enemy.Bases),
                    CurrentSquad = enemy.CurrentSquad,
                };
                item.Id = enemy.CurrentSquad.ToString();

                item.Init(this);
                _enemys.Add(item);
            }

            _aiModule.Init(_enemys);
        }

        public void ActiveState(Squad squad)
        {
            List<BaseView> bases = _basesController.GetBases(squad);
            int selectCount = Random.Range(0, bases.Count);

            List<BaseView> playerBases = _basesController.GetBases(Squad.Player);
            if (playerBases == null || playerBases.Count <= 0) return;
            int playerNum = Random.Range(0, playerBases.Count - 1);
            List<BaseView> selectedBases = new List<BaseView>();
            for (int i = 0; i < selectCount; i++)
            {
                int num = Random.Range(0, bases.Count - 1);
                selectedBases.Add(bases[num]);
            }
            _unitsManager.CreateUnits(selectedBases, playerBases[playerNum]);
        }
    }

    public class AIItem : AIEmptyItem
    {
        private AIController _aiController;

        public List<BaseData> BasesData = new List<BaseData>();
        public List<BaseView> Bases = new List<BaseView>();
        public Squad CurrentSquad;

        private AIState _currentSate;

        private float _minTimeAwait = 3f;
        private float _maxTimeAwait = 5f;
        private float _timeAwait = 0;
        private float _currentTime = 0;

        public void Init(AIController ai)
        {
            _aiController = ai;
            _timeAwait = Random.Range(_minTimeAwait, _maxTimeAwait);
            SetState(new IdleAIState());
        }

        public void SetState(AIState aIState)
        {
            _currentSate = aIState;
            _currentSate.AIItem = this;
        }

        public override void UpdateAI()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _timeAwait)
            {
                _timeAwait = Random.Range(_minTimeAwait, _maxTimeAwait);
                _currentTime = 0;
                _currentSate.ActiveState(_aiController);
            }
        }
    }
}
