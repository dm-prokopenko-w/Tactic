using UnityEngine;
using BaseSystem;
using System;
using GameplaySystem;

namespace UnitSystem
{
    public class Unit : ItemView
    {
        private Base _targetBase;
        private Vector3 _target;
        private float _speed = 5f;
        private Action<Unit> OnTrigger;

        public void SetTarget(Base target, Base startBase, Action<Unit> onTrigger)
        {
            _squadItem = startBase.GetSquad();
            _targetBase = target;
            _target = target.transform.position;
            ChangeSquad(startBase.GetColor(), startBase.GetSquad());
            OnTrigger = onTrigger;
        }

        public Base GetTargetBase() => _targetBase;

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