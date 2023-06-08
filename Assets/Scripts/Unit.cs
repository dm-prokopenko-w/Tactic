using System;
using UnityEngine;

public class Unit : ItemView
{
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Rigidbody2D _rb;

    protected TypeItemView _type => TypeItemView.Unit;

    private Collider2D _startCol;
    private Base _targetBase;
    private Vector3 _target;
    private float _speed = 5f;
    private Action<Collider2D, Collider2D> _onTrigger;

    public void SetTarget(Base target, Base startBase, Action<Collider2D, Collider2D> onTrigger)
    {
        SquadItem = startBase.SquadItem;
        _targetBase = target;
        _target = target.transform.position;
        _onTrigger = onTrigger;
        _startCol = startBase.GetCollider();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col != _startCol)
        {
            _onTrigger?.Invoke(_collider, col);
        }
    }

    public override TypeItemView GetTypeItemView() => _type;
    public override string GetTargetId() => _targetBase.Id;

    public Collider2D GetCollider() => _collider;
    public Base GetTargetBase() => _targetBase;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speed);
    }
}