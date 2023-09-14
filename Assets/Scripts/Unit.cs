using System;
using UnityEngine;
using BaseSystem;
using UnityEngine.UI;

namespace UnitSystem
{
    public class Unit : ItemView
    {
        private Collider2D _startCol;
        private Base _targetBase;
        private Vector3 _target;
        private float _speed = 5f;
        private Action<Collider2D, Collider2D> _onTrigger;

        public void SetTarget(Base target, Base startBase, Action<Collider2D, Collider2D> onTrigger)
        {
            _squadItem = startBase.GetSquad();
            _targetBase = target;
            _target = target.transform.position;
            _onTrigger = onTrigger;
            _startCol = startBase.GetCollider();
            ChangeColor(startBase.GetColor());
            SetSquad(startBase.GetSquad());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col != _startCol)
            {
                _onTrigger?.Invoke(_collider, col);
            }
        }

        public Base GetTargetBase() => _targetBase;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speed);
        }
    }
}