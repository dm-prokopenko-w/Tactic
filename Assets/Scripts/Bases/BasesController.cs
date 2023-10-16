using System;
using System.Collections.Generic;
using UnityEngine;
using GameplaySystem;
using VContainer.Unity;
using VContainer;
using System.Threading.Tasks;
using Game;
using Core.ControlSystem;

namespace BaseSystem
{
    public class BasesController : ITickable
    {
        [Inject] private AssetLoader _assetLoader;
        [Inject] private ControlModule _control;

        public Action OnUpdateBases;
        public Action<Squad> OnChangeSquad;
        public Action<BaseItem, Squad> OnChangeBaseSquad;
        public Action OnInit;

        private List<BaseItem> _bases = new List<BaseItem>();
        private float _currentTime;
        private float _step = 1f;

        public async Task Init(Action<Squad> onChangeSquad)
        {
            GameData data = await _assetLoader.LoadConfig(GameConstants.GameData) as GameData;
            foreach (var b in _bases)
            {
                var baseData = data.GetDataById(b.Raion);
                if (baseData == null) continue;
                b.Init(baseData);
            }

            OnChangeSquad = onChangeSquad;
            OnInit?.Invoke();
        }

        public void AddBase(BaseView view)
        {
            var item = new BaseItem(view, OnChangeBaseSquad);
            OnUpdateBases += item.UpdateCounter;

            _control.TouchStart += item.SelectBase;
            _control.TouchMoved += item.SelectBase;
            _control.TouchEnd += item.End;

            _bases.Add(item);
        }

        public void SelectedBase(RaycastHit2D hit, Squad squad)
        {
            var b = _bases.Find(x => x.GetCollider() == hit.collider);
            if (b == null) return;
            if (squad == Squad.None)
            {
                if (OnChangeSquad != null)
                {
                    OnChangeSquad.Invoke(b.CurrentSquad);
                }
            }

            if (squad != b.CurrentSquad || b.IsSelectedBase()) return;

            b.SelectedBase(true);
        }

        public List<BaseItem> GetBases(Squad squad) => _bases.FindAll(x => x.CurrentSquad == squad);

        public void Tick()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _step)
            {
                OnUpdateBases?.Invoke();
                _currentTime = 0;
            }
        }

        public void UnSelectedBases()
        {
            if (_bases != null)
            {
                foreach (var b in _bases)
                {
                    b.SelectedBase(false);
                }
            }

            if (OnChangeSquad != null)
            {
                OnChangeSquad.Invoke(Squad.None);
            }
        }

        public (BaseItem, List<BaseItem>) GetBasesForCreateUnits(Collider2D col)
        {
            var targetBase = _bases.Find(x => x.GetCollider() == col);
            var selectedBase = _bases.FindAll(x => x.IsSelectedBase());
            selectedBase.Remove(targetBase);
            return (targetBase, selectedBase);
        }
    }
}