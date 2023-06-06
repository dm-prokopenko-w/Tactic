using TMPro;
using UnityEngine;
using Zenject;

public class Base : Item
{
    [Inject] private GameplayController _gameplayController;

    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private TextMeshProUGUI _counter;

    private int _countUnits = 10;

    private void Start()
    {
        _gameplayController.AddBase(this);
        UpdateCounter();
    }

    public Rigidbody2D GetRigidbody() => _rb;

    public int GetCountUnits()
    {
        _countUnits -= _countUnits / 2;
        UpdateCounter();
        return _countUnits;
    }

    public void AddedUnit(int count)
    {
        _countUnits += count;
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        _counter.text = _countUnits.ToString();
    }
}