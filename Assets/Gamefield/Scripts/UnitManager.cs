using BaseSystem;
using Core;
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

        public void CreateUnit(Base startBase, Base targetBase)
        {
            int count = startBase.GetMovedCountUnits();
            for (int i = 0; i < count; i++)
            {
                var p = startBase.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
                var unit = _poolModule.Spawn(_unitPrefab, p, Quaternion.identity, _parent);
                var unitScript = unit.GetComponent<Unit>();
                unitScript.SetTarget(targetBase, startBase, OnTriggerWithBase);
            }

            startBase.SelectedBase(false);
        }

        private void OnTriggerWithBase(Unit unit)
        {
             OnTriggerWithBase(unit, unit.GetTargetBase());
            if (unit == null) return;
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
                    targetItem.ChangeSquad(unit.GetColor(), unit.GetSquad());
                    targetItem.AddedUnit(1);
                }
            }
        }

    }
}
