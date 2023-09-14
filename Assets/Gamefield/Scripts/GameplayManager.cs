using System;
using System.Collections.Generic;
using BaseSystem;
using Core;
using UnityEngine;
using UnitSystem;

public class GameplayManager
{
    private BasesController _basesController;
    private List<GameplayData> _data;
    private Action<Collider2D, Collider2D> _onTrigger;
    private List<Base> _bases = new List<Base>();
    private List<Unit> _movedUnits = new List<Unit>();
    private ObjectPoolModule _poolModule;
    private Transform _parent;

    public GameplayManager(GameObject unitPrefab, ObjectPoolModule poolModule, Transform parent)
    {
        _basesController = new BasesController(unitPrefab, poolModule, parent);
        _basesController.OnTrigger += CheckOnTrigger;
        _poolModule = poolModule;
        _parent = parent;
    }

    public void Unsubscribe()
    {
        _basesController.OnTrigger -= CheckOnTrigger;
    }

    public void AddedNewBase(Base b)
    {
        _bases.Add(b);
        _basesController.AddedNewBase(b);
    }

    public void SelectedBase(RaycastHit2D hit) => _basesController.SelectedBase(hit);

    public void CreateUnits(RaycastHit2D hit)
    {
        List<Unit> units = _basesController.CreateUnits(hit);
        foreach (var unit in units)
        {
            _movedUnits.Add(unit);
        }
    }

    public void UpdateUnits(Action onUpdateBases) => _basesController.UpdateUnits(onUpdateBases);

    public void CheckOnTrigger(Collider2D thisCol, Collider2D targetCol)
    {
        Unit unit = _movedUnits.Find(x => x.GetCollider() == thisCol);
        Base targetItem = _bases.Find(x => x.GetCollider() == targetCol);

        if (unit == null || targetCol == null) return;

        OnTriggerWithBase(unit, targetItem);
        unit.transform.SetParent(_parent);
        _poolModule.Despawn(unit.gameObject);
    }

    private void OnTriggerWithBase(Unit unit, Base targetItem)
    {
        if (unit.GetSquad() == targetItem.GetSquad())
        {
            if (unit.GetTargetBase().GetCollider() == targetItem.GetCollider())
            {
                targetItem.AddedUnit(1);
            }
        }
        else
        {
            if (targetItem.GetCountUnits() > 0)
            {
                targetItem.AddedUnit(-1);
            }
            else
            {
                ChangeSquad(targetItem, unit.GetSquad(), unit.GetColor());
                targetItem.AddedUnit(1);
            }
        }

        _movedUnits.Remove(unit);
    }

    private void ChangeSquad(Base item, Squad newSquad, Color color)
    {
        item.SetSquad(newSquad);
        item.ChangeColor(color);
    }
}