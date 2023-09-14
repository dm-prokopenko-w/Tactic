using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnitSystem;
using Random = UnityEngine.Random;

namespace BaseSystem
{
    public class BasesController
    {
        public Action<Collider2D, Collider2D> OnTrigger;

        private ObjectPoolModule _poolModule;
        private GameObject _unitPrefab;

        private List<Base> _bases;
        private Squad _currentSquad = Squad.None;
        private float _currentTime;
        private float _step = 1f;
        private Transform _parent;

        public BasesController(GameObject unitPrefab, ObjectPoolModule poolModule, Transform parent)
        {
            _unitPrefab = unitPrefab;
            _poolModule = poolModule;
            _bases = new List<Base>();
            _parent = parent;
        }

        public void AddedNewBase(Base b) => _bases.Add(b);

        public void SelectedBase(RaycastHit2D hit)
        {
            var b = _bases.Find(x => x.GetRigidbody() == hit.rigidbody);
            if (b == null) return;
            if (_currentSquad == Squad.None)
            {
                _currentSquad = b.GetSquad();
            }

            if (_currentSquad != b.GetSquad()) return;

            b.SelectedBase(true);
        }

        public List<Unit> CreateUnits(RaycastHit2D hit)
        {
            Base targetBase = _bases.Find(x => x.GetRigidbody() == hit.rigidbody);

            var bases = new List<Base>(_bases);
            bases.Remove(targetBase);
            var units = new List<Unit>();

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
                        unitScript.SetTarget(targetBase, b, OnTrigger);
                        units.Add(unitScript);
                    }

                    b.SelectedBase(false);
                }
            }

            _currentSquad = Squad.None;
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
}