using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameplaySystem;
using VContainer.Unity;

namespace BaseSystem
{
    public class BasesController : ITickable
    {
        public Action OnUpdateBases;
        public Action<Squad> OnChangeSquad;

        private List<Base> _bases = new List<Base>();
        private float _currentTime;
        private float _step = 1f;

        public void Init(Base[] bases, Action<Squad> onChangeSquad)
        {
            foreach (var b in bases)
            {
                b.Init();
            }

            _bases = bases.ToList();
            OnChangeSquad = onChangeSquad;
        }

        public void SelectedBase(RaycastHit2D hit, Squad squad)
        {
            var b = _bases.Find(x => x.GetCollider() == hit.collider);
            if (b == null) return;
            if (squad == Squad.None)
            {
                if (OnChangeSquad != null)
                {
                    OnChangeSquad.Invoke(b.GetSquad());
                }
            }

            if (squad != b.GetSquad() || b.IsSelectedBase()) return;

            b.SelectedBase(true);
        }

        public List<Base> GetBases(Squad squad) => _bases.FindAll(x => x.GetSquad() == squad);

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

        public (Base, List<Base>) GetBasesForCreateUnits(Collider2D col)
        {
            Base targetBase = _bases.Find(x => x.GetCollider() == col);
            var selectedBase = _bases.FindAll(x => x.IsSelectedBase());
            selectedBase.Remove(targetBase);
            return (targetBase, selectedBase);
        }
    }
}