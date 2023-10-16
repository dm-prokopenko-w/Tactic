using UnityEngine;
using BaseSystem;
using System;
using GameplaySystem;

namespace UnitSystem
{
    public class UnitView : ItemView
    {
        private BaseItem _targetBase;
        private Vector3 _target;
        private float _speed = 5f;
        private Action<UnitView> OnTrigger;

        public void SetTarget(BaseItem startBase, BaseItem target, Action<UnitView> onTrigger)
        {
            _squadItem = startBase.CurrentSquad;
            _targetBase = target;
            _target = target.GetBasePos();
            ChangeSquad(startBase.CurrentColor, startBase.CurrentSquad);
            OnTrigger = onTrigger;
        }

        public BaseItem GetTargetBase() => _targetBase;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col != _targetBase.GetCollider()) return;

            OnTrigger.Invoke(this);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speed);
        }
    }
}