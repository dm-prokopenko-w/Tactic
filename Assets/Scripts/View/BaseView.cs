using TMPro;
using UnityEngine;
using VContainer;
using Core.ControlSystem;
using UnityEngine.EventSystems;
using GameplaySystem;
using System.Threading.Tasks;
using Game.Configs;

namespace BaseSystem
{
    public class BaseView : ItemView
    {
        [Inject] private ControlModule _control;
        [Inject] private BasesController _basesController;

        public Raions Raion
        {
            get
            {
                return _raion;
            }
            private set
            {
                _raion = value;
            }
        }

        [SerializeField] private TextMeshPro _counter;
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Raions _raion;

        private int _countUnits;
        private bool _isSelected = false;

        public void Init(DataBase dataBase)
        {
            Subscribe();

            InitLine();

            _countUnits = dataBase.CountOnStart;

            ChangeSquad(dataBase.ColorSquad, dataBase.CurrentSquad);
            UpdateCounter();
        }

        private void InitLine()
        {
            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, transform.position);
        }

        private void Subscribe()
        {
            if (_basesController != null)
            {
                _basesController.OnUpdateBases += UpdateBaseCountUnits;
            }

            _control.TouchStart += SelectBase;
            _control.TouchMoved += SelectBase;
            _control.TouchEnd += End;
        }

        private void OnDestroy()
        {
            if (_basesController != null)
            {
                _basesController.OnUpdateBases -= UpdateBaseCountUnits;
            }

            _control.TouchStart -= SelectBase;
            _control.TouchMoved -= SelectBase;
            _control.TouchEnd -= End;
        }

        public int GetCountUnits() => _countUnits;

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

        public override void ChangeSquad(Color color, Squad s)
        {
            _basesController.OnChangeBaseSquad?.Invoke(this, s);
            base.ChangeSquad(color, s);
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