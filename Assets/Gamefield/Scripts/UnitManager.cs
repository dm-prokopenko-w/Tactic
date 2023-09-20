using BaseSystem;
using Core;
using System.Collections.Generic;
using System.Linq;
using UnitSystem;
using UnityEngine;

namespace GameplaySystem
{
    public class UnitsManager
    {
        private List<Base> _bases = new List<Base>();
        private ObjectPoolModule _poolModule;
        private GameObject _unitPrefab;
        private Transform _parent;
        private GameplayManager _gameplay;

        public UnitsManager(GameObject unitPrefab, Transform parent, ObjectPoolModule poolModule, Base[] bases, GameplayManager gameplay)
        {
            _gameplay = gameplay;
            _unitPrefab = unitPrefab;
            _parent = parent;
            _poolModule = poolModule;
            _bases = bases.ToList();
        }

        public void CreateUnits(RaycastHit2D hit, Squad squad)
        {
            Base targetBase = _bases.Find(x => x.GetCollider() == hit.collider);

            var bases = new List<Base>(_bases);
            bases.Remove(targetBase);

            foreach (Base b in bases)
            {
                if (b.IsSelectedBase())
                {
                    int count = b.GetMovedCountUnits();
                    for (int i = 0; i < count; i++)
                    {
                        var p = b.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
                        var unit = _poolModule.Spawn(_unitPrefab, p, Quaternion.identity, _parent);
                        var unitScript = unit.GetComponent<Unit>();
                        unitScript.SetTarget(targetBase, b, OnTriggerWithBase);
                    }

                    b.SelectedBase(false);
                }
            }
        }

        private void OnTriggerWithBase(Unit unit)
        {
             _gameplay.OnTriggerWithBase(unit, unit.GetTargetBase());
            if (unit == null) return;
            unit.transform.SetParent(_parent);
            _poolModule.Despawn(unit.gameObject);
        }
    }
}
