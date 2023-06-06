using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Random = UnityEngine.Random;

public class GameplayController : MonoInstaller
{
    [Inject] private ControlModule _control;
    [Inject] private ObjectPoolModule _poolModule;

    [SerializeField] private List<Base> _bases = new List<Base>();
    [SerializeField] private List<Base> _selectedBases = new List<Base>();
    [SerializeField] private GameObject _unitPrefab;

    private Vector2 _lastPos;
    private Squad _currentSquad = Squad.None;

    public override void InstallBindings()
    {
        Container.Bind<GameplayController>().FromInstance(this).AsSingle().NonLazy();
    }

    private void Start()
    {
        _control.TouchStart += TouchStart;
        _control.TouchEnd += TouchEnd;
        _control.TouchMoved += TouchMoved;
    }

    private void OnDestroy()
    {
        _control.TouchStart -= TouchStart;
        _control.TouchEnd -= TouchEnd;
        _control.TouchMoved -= TouchMoved;
    }

    public void AddBase(Base b) => _bases.Add(b);

    private void TouchStart(PointerEventData eventData)
    {
    }

    private void TouchMoved(PointerEventData eventData)
    {
        AddedBases(eventData);
    }

    private void TouchEnd(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100.0f);
        if (hit != null && hit.collider != null)
        {
            var b = _bases.Find(x => x.GetRigidbody() == hit.rigidbody);
            CreateUnits(b.transform, b);
        }
    }

    private void AddedBases(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100.0f);
        if (hit != null && hit.collider != null)
        {
            var b = _bases.Find(x => x.GetRigidbody() == hit.rigidbody);
            if (_selectedBases.Count == 0 && _currentSquad == Squad.None)
            {
                _currentSquad = b.SquadItem;
            }

            if (_selectedBases.Contains(b)
                || b == null
                || !_currentSquad.ToString().Equals(b.SquadItem.ToString()))
            {
                return;
            }

            _selectedBases.Add(b);
        }
    }

    private void CreateUnits(Transform target, Base targetBase)
    {
        foreach (var b in _selectedBases)
        {
            int count = b.GetCountUnits();
            for (int i = 0; i < count; i++)
            {
                var p = b.transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));
                var unit = _poolModule.Spawn(_unitPrefab, p, Quaternion.identity, b.transform);
                var onTarget = new Action(() =>
                {
                    _poolModule.Despawn(unit);
                    unit.transform.SetParent(target);
                    targetBase.AddedUnit(1);
                });
                unit.GetComponent<Unit>().SetTarget(target, b.SquadItem, onTarget);
            }
        }

        _currentSquad = Squad.None;
        _selectedBases.Clear();
    }
}