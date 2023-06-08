using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasesController
{
    public Action<Collider2D, Collider2D> OnTrigger;
    
    private ObjectPoolModule _poolModule;
    private GameObject _unitPrefab;

    private List<Base> _bases;
    private List<Base> _selectedBases;
    private Squad _currentSquad = Squad.None;
    private float _currentTime;
    private float _step = 1f;
    private Transform _parent;
    
    public BasesController(GameObject unitPrefab, ObjectPoolModule poolModule, Transform parent)
    {
        _unitPrefab = unitPrefab;
        _poolModule = poolModule;
        _bases = new List<Base>();
        _selectedBases = new List<Base>();
        _parent = parent;
    }

    public void AddedNewBase(Base b) => _bases.Add(b);

    public void SelectedBase(RaycastHit2D hit)
    {
        var b = _bases.Find(x => x.GetRigidbody() == hit.rigidbody);
        if (b == null) return;

        if (_selectedBases.Count == 0 && _currentSquad == Squad.None)
        {
            _currentSquad = b.SquadItem;
        }

        if (_selectedBases.Contains(b) || b == null || _currentSquad != b.SquadItem) return;

        _selectedBases.Add(b);
    }

    public List<Unit> CreateUnits(RaycastHit2D hit)
    {
        Base targetBase = _bases.Find(x => x.GetRigidbody() == hit.rigidbody);

        if (_selectedBases.Contains(targetBase))
        {
            _selectedBases.Remove(targetBase);
        }

        var units = new List<Unit>();
        foreach (var b in _selectedBases)
        {
            int count = b.GetMovedCountUnits();
            for (int i = 0; i < count; i++)
            {
                var p = b.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
                var unit = _poolModule.Spawn(_unitPrefab, p, Quaternion.identity, _parent);
                var unitScript = unit.GetComponent<Unit>();
                unitScript.SetTarget(targetBase, b, OnTrigger);
                units.Add(unitScript);
            }
        }

        _currentSquad = Squad.None;
        _selectedBases.Clear();
        return units;
    }

    public void UpdateUnits(Action onUpdateBases)
    {
        _currentTime += Time.deltaTime;
        
        if (_currentTime > _step)
        {
            onUpdateBases?.Invoke();
            _currentTime = 0;
        }
    }
}