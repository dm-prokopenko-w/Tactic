using BaseSystem;
using Core;
using System.Collections.Generic;
using UnitSystem;
using UnityEngine;
using VContainer;

namespace GameplaySystem
{
    public class UnitsManager
    {
        [Inject] private ObjectPoolModule _poolModule;

        private GameObject _unitPrefab;
        private Transform _parent;

        public void Init(GameObject unitPrefab, Transform parent)
        {
            _unitPrefab = unitPrefab;
            _parent = parent;
        }

        public void CreateUnits(List<BaseView> startBases, BaseView targetBase)
        {
            foreach (BaseView b in startBases)
            {
                int count = b.GetMovedCountUnits();
                for (int i = 0; i < count; i++)
                {
                    var p = b.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
                    var unit = _poolModule.Spawn(_unitPrefab, p, Quaternion.identity, _parent);
                    var unitScript = unit.GetComponent<UnitView>();
                    unitScript.SetTarget(targetBase, b, OnTriggerWithBase);
                }

                b.SelectedBase(false);
            }

            targetBase.SelectedBase(false);
        }

        private void OnTriggerWithBase(UnitView unit)
        {
            OnTriggerWithBase(unit, unit.GetTargetBase());
            if (unit == null) return;
            unit.transform.SetParent(_parent);
            _poolModule.Despawn(unit.gameObject);
        }

        private void OnTriggerWithBase(UnitView unit, BaseView targetItem)
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
                    targetItem.ChangeSquad(unit.GetColor(), unit.GetSquad());
                    targetItem.AddedUnit(1);
                }
            }
        }

    }
}
