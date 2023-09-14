using TMPro;
using UnityEngine;
using Zenject;
using Core;
using UnityEngine.EventSystems;

namespace BaseSystem
{
    public class Base : ItemView
    {
        [Inject] private GamefieldController _gamefieldController;
        [Inject] private ControlModule _control;

        [SerializeField] private TextMeshProUGUI _counter;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private LineRenderer _line;
        [SerializeField] private BaseData _data;

        private int _countUnits;
        private bool _isSelected = false;

        private void Start()
        {
            _gamefieldController.AddBase(this);
            UpdateCounter();
            _gamefieldController.OnUpdateBases += UpdateUnits;

            _control.TouchMoved += Drag;
            _control.TouchEnd += End;

            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, transform.position);

            _countUnits = _data.CountOnStart;
            ChangeColor(_data.ColorSquad);
            SetSquad(_data.SquadType);
        }

        private void OnDestroy()
        {
            _gamefieldController.OnUpdateBases -= UpdateUnits;
            _control.TouchMoved -= Drag;
            _control.TouchEnd -= End;
        }

        public override int GetCountUnits() => _countUnits;

        public Rigidbody2D GetRigidbody() => _rb;
        public void SelectedBase(bool value) => _isSelected = value;
        public bool IsSelectedBase() => _isSelected;

        private void Drag(PointerEventData data)
        {
            if (!_isSelected) return;
            var p = Camera.main.ScreenToWorldPoint(data.position);
            _line.SetPosition(1, new Vector3(p.x, p.y, transform.position.z));
        }

        private void End(PointerEventData data)
        {
            _line.SetPosition(1, transform.position);
            _isSelected = false;
        }

        public int GetMovedCountUnits()
        {
            int count = _countUnits / 2;
            _countUnits -= count;
            UpdateCounter();
            return count;
        }

        public override void AddedUnit(int count)
        {
            _countUnits += count;
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _counter.text = _countUnits.ToString();
        }

        private protected void UpdateUnits()
        {
            _countUnits++;
            UpdateCounter();
        }
    }
}