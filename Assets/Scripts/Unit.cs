using System;
using UnityEngine;

public class Unit : ItemView
{
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Rigidbody2D _rb;
    private Collider2D _startCol;
    private Transform _target;
    private float _speed =  5f;

    private Action<Collider2D> _onTarget;
    
    public void SetTarget(Transform target, Base startBase, Action<Collider2D> onTarget)
    {
        SquadItem = startBase.SquadItem;
        _startCol = startBase.GetCollider();
        _target = target;
        _onTarget = onTarget;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var item = col.GetComponent<ItemView>();
        if (item != null && _startCol != col)
        {
            _onTarget?.Invoke(col);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);
    }
}
