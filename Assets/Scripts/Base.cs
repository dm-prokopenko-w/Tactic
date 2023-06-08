using TMPro;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class Base : ItemView
{
    [Inject] private GamefieldController _gamefieldController;

    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private RectTransform _parent;
    
    protected TypeItemView _type => TypeItemView.Base;

    private int _countUnits = 10;

    private void Start()
    {
        _gamefieldController.AddBase(this);
        UpdateCounter();
        _gamefieldController.OnUpdateBases += UpdateUnits;
    }

    private void OnDestroy()
    {
        _gamefieldController.OnUpdateBases -= UpdateUnits;
    }

    public override TypeItemView GetTypeItemView() => _type;
    public override int GetCountUnits() => _countUnits;

    public CircleCollider2D GetCollider() => _collider;
    public Rigidbody2D GetRigidbody() => _rb;

    public int GetMovedCountUnits()
    {
        int count = _countUnits / 2;
        _countUnits -= count;
        UpdateCounter();
        return count;
    }

    public override void AddedUnit(int count)
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