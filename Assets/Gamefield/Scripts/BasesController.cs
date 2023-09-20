using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BaseSystem
{
    public class BasesController
    {
        public Action OnUpdateBases;
        public Action<Squad> OnChangeSquad;

        private List<Base> _bases = new List<Base>();
        private float _currentTime;
        private float _step = 1f;

        public BasesController(Base[] bases, Action<Squad> onChangeSquad)
        {
            foreach (var b in bases)
            {
                b.Init(this);
            }

            _bases = bases.ToList();
            OnChangeSquad = onChangeSquad;
        }

        public void SelectedBase(RaycastHit2D hit, Squad squad)
        {
            var b = _bases.Find(x => x.GetRigidbody() == hit.rigidbody);
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

        public void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _step)
            {
                OnUpdateBases?.Invoke();
                _currentTime = 0;
            }
        }
    }
}