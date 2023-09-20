using TMPro;
using UnityEngine;
using VContainer;
using Core;
using UnityEngine.EventSystems;

namespace BaseSystem
{
    public class Base : ItemView
    {
        [Inject] private ControlModule _control;

        [SerializeField] private TextMeshProUGUI _counter;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private LineRenderer _line;
        [SerializeField] private BaseData _data;

        private int _countUnits;
        private bool _isSelected = false;
        private BasesController _basesController;

        public void Init(BasesController basesController)
        {
            _control.TouchStart += SelectBase;
            _control.TouchMoved += SelectBase;
            _control.TouchEnd += End;

            _basesController = basesController;

            basesController.OnUpdateBases += UpdateBaseCountUnits;

            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, transform.position);

            _countUnits = _data.CountOnStart;

            ChangeColor(_data.ColorSquad);
            SetSquad(_data.SquadType);
            UpdateCounter();
        }

        private void OnDestroy()
        {
            _basesController.OnUpdateBases += UpdateBaseCountUnits;

            _control.TouchStart -= SelectBase;
            _control.TouchMoved -= SelectBase;
            _control.TouchEnd -= End;
        }

        public int GetCountUnits() => _countUnits;

        public Rigidbody2D GetRigidbody() => _rb;
        public void SelectedBase(bool value) => _isSelected = value;

        public bool IsSelectedBase() => _isSelected;

        private void SelectBase(PointerEventData data)
        {
            if (!_isSelected) return;
            var p = Camera.main.ScreenToWorldPoint(data.position);
            _line.SetPosition(1, new Vector3(p.x, p.y, transform.position.z));
        }

        private void End(PointerEventData data)
        {
            _line.SetPosition(1, transform.position);
        }

        public int GetMovedCountUnits()
        {
            int count = _countUnits / 2;
            _countUnits -= count;
            UpdateCounter();
            return count;
        }

        public void AddedUnit(int count)
        {
            _countUnits += count;
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _counter.text = _countUnits.ToString();
        }

        private void UpdateBaseCountUnits()
        {
            _countUnits++;
            UpdateCounter();
        }
    }
}