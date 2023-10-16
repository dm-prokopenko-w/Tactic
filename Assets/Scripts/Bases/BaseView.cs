using TMPro;
using UnityEngine;
using VContainer;
using GameplaySystem;

namespace BaseSystem
{
    public class BaseView : ItemView
    {
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

        [Inject]
        public void Construct()
        {
            _basesController.AddBase(this);
        }

        public void Init()
        {
            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, transform.position);
        }

        public void SetLine(Vector3 pos) => _line.SetPosition(1, pos);

        public void UpdateCounter(int count) => _counter.text = count.ToString();
    }
}