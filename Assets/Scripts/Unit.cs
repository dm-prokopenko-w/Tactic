using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Item
{
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Rigidbody2D _rb;
    private Collider2D _col;
    private Transform _target;
    private float _speed = 0.001f;

    private List<Collider2D> _selectedBases;
    private Action _onTarget;
    
    public void SetTarget(Transform target, Squad squad, Action onTarget)
    {
        SquadItem = squad;
        _target = target;
        _onTarget = onTarget;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var item = col.GetComponent<Item>();
        if (item != null && !SquadItem.ToString().Equals(item.SquadItem.ToString()))
        {
            _onTarget?.Invoke();
        }
    }

    private void Update()
    {
        transform.Translate(_target.position * _speed);
    }
}
