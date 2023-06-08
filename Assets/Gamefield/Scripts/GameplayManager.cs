using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class GameplayManager
{
    private BasesController _basesController;
    private List<GameplayData> _data;
    private Action<Collider2D, Collider2D> _onTrigger;
    private Dictionary<Collider2D, ItemView> _items = new Dictionary<Collider2D, ItemView>();
    private Dictionary<Collider2D, Unit> _movedUnits = new Dictionary<Collider2D, Unit>();
    private ObjectPoolModule _poolModule;
    private Transform _parent;
    
    public GameplayManager(List<GameplayData> data, GameObject unitPrefab, ObjectPoolModule poolModule, Transform parent)
    {
        _data = data;
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
        _items.Add(b.GetCollider(), b);
        _basesController.AddedNewBase(b);
    }

    public void SelectedBase(RaycastHit2D hit) => _basesController.SelectedBase(hit);

    public void CreateUnits(RaycastHit2D hit)
    {
        List<Unit> units = _basesController.CreateUnits(hit);
        foreach (var unit in units)
        {
            _items.Add(unit.GetCollider(), unit);
        }
    }

    public void UpdateUnits(Action onUpdateBases) => _basesController.UpdateUnits(onUpdateBases);

    public void CheckOnTrigger(Collider2D thisCol, Collider2D targetCol)
    {
        _items.TryGetValue(thisCol, out ItemView unit);
        _items.TryGetValue(targetCol, out ItemView targetItem);
        if(unit == null || targetCol == null) return;

        switch (targetItem.GetTypeItemView())
        {
            case TypeItemView.Unit:
                break;
            case TypeItemView.Base:
                OnTriggerWithBase(unit, targetItem);
                break;
        }

        _items.Remove(thisCol);
        unit.transform.SetParent(_parent);
        _poolModule.Despawn(unit.gameObject);
    }

    private void OnTriggerWithBase(ItemView unit, ItemView targetItem)
    {
        if (unit.SquadItem == targetItem.SquadItem)
        {
            if (unit.GetTargetId().Equals(targetItem.Id))
            {
                targetItem.AddedUnit(1);
            }
        }
        else
        {
            if(targetItem.GetCountUnits() > 0)
            {
                targetItem.AddedUnit(-1);
            }
            else
            {
                ChangeSquad(targetItem, unit.SquadItem);
                targetItem.AddedUnit(1);
            }
        }
    }

    private void ChangeSquad(ItemView item, Squad newSquad)
    {
        item.SquadItem = newSquad;
        var color = _data.Find(x => x.SquadData == newSquad).ColorData;
        Debug.LogError(color);
        item.ChangeColor(color);
    }
}