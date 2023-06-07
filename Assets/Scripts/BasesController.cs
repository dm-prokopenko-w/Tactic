using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasesController
{
    private ObjectPoolModule _poolModule;
    private GameObject _unitPrefab;

    private List<Base> _bases;
    private List<Base> _selectedBases;
    private Squad _currentSquad = Squad.None;
    private float _currentTime;
    private float _step = 1f;
    
    public BasesController(GameObject unitPrefab, ObjectPoolModule poolModule)
    {
        _unitPrefab = unitPrefab;
        _poolModule = poolModule;

        _bases = new List<Base>();
        _selectedBases = new List<Base>();
    }

    public void AddedNewBase(Base b) => _bases.Add(b);

    public void AddedBases(RaycastHit2D hit)
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

    public void CreateUnits(RaycastHit2D hit)
    {
        var targetBase = _bases.Find(x => x.GetRigidbody() == hit.rigidbody);

        if (_selectedBases.Contains(targetBase))
        {
            _selectedBases.Remove(targetBase);
        }

        foreach (var b in _selectedBases)
        {
            int count = b.GetCountUnits();
            for (int i = 0; i < count; i++)
            {
                var p = b.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
                var unit = _poolModule.Spawn(_unitPrefab, p, Quaternion.identity, targetBase.GetParent());
                var unitScript = unit.GetComponent<Unit>();
                var onTarget = new Action<Collider2D>((col) =>
                {
                    var squad = col.GetComponent<ItemView>().SquadItem;

                    if (unitScript.SquadItem == squad)
                    {
                        targetBase.AddedUnit(1);
                    }
                    else
                    {
                        targetBase.AddedUnit(-1);
                    }

                    _poolModule.Despawn(unit);
                    unit.transform.SetParent(targetBase.GetParent());
                });
                unitScript.SetTarget(targetBase.GetParent(), b, onTarget);
            }
        }

        _currentSquad = Squad.None;
        _selectedBases.Clear();
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

    public void ChangeSquad(Squad newSquad)
    {
        
    }
}