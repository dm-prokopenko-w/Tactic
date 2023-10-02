using UnityEngine;
using BaseSystem;
using System;
using GameplaySystem;

namespace UnitSystem
{
    public class UnitView : ItemView
    {
        private BaseView _targetBase;
        private Vector3 _target;
        private float _speed = 5f;
        private Action<UnitView> OnTrigger;

        public void SetTarget(BaseView target, BaseView startBase, Action<UnitView> onTrigger)
        {
            _squadItem = startBase.GetSquad();
            _targetBase = target;
            _target = target.transform.position;
            ChangeSquad(startBase.GetColor(), startBase.GetSquad());
            OnTrigger = onTrigger;
        }

        public BaseView GetTargetBase() => _targetBase;

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