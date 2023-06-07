using System;
using TMPro;
using UnityEngine;
using Zenject;

public class Base : ItemView
{
    [Inject] private GameplayController _gameplayController;

    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private RectTransform _parent;

    private int _countUnits = 10;

    private void Start()
    {
        _gameplayController.AddBase(this);
        UpdateCounter();
        _gameplayController.OnUpdateBases += UpdateUnits;
    }

    private void OnDestroy()
    {
        _gameplayController.OnUpdateBases -= UpdateUnits;
    }

    public CircleCollider2D GetCollider() => _collider;
    public Rigidbody2D GetRigidbody() => _rb;
    public RectTransform GetParent() => _parent;

    public int GetCountUnits()
    {
        int count = _countUnits / 2;
        _countUnits -= count;
        UpdateCounter();
        return count;
    }

    public void AddedUnit(int count)
    {
        _countUnits += count;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = _countUnits.ToString();
    }

    private protected void UpdateUnits()
    {
        _countUnits++;
        UpdateCounter();
    }
}