using System;
using Core;
using UnityEngine;
using BaseSystem;
using UnityEngine.EventSystems;
using VContainer;
using System.Linq;

namespace GameplaySystem
{
    public class GamefieldController : Moduls
    {
        [Inject] private ControlModule _control;
        [Inject] private ObjectPoolModule _poolModule;

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _parent;

        private GameplayManager _gameplay;
        private BasesController _basesController;
        private UnitsManager _unitsManager;
        private Squad _currentSquad = Squad.None;


        private void Start()
        {
            var bases = FindObjectsOfType<Base>();
            _gameplay = new GameplayManager();
            _basesController = new BasesController(bases, ChangeSquad);
            _unitsManager = new UnitsManager(_unitPrefab, _parent, _poolModule, bases, _gameplay);

            _control.TouchStart += TouchStart;
            _control.TouchEnd += TouchEnd;
            _control.TouchMoved += TouchMoved;
        }

        private void ChangeSquad(Squad squad) => _currentSquad = squad;

        private void OnDestroy()
        {
            _control.TouchStart -= TouchStart;
            _control.TouchEnd -= TouchEnd;
            _control.TouchMoved -= TouchMoved;
        }

        private void TouchStart(PointerEventData eventData)
        {
            ThrowRay(eventData, _basesController.SelectedBase);
        }

        private void TouchMoved(PointerEventData eventData)
        {
            ThrowRay(eventData, _basesController.SelectedBase);
        }

        private void TouchEnd(PointerEventData eventData)
        {
            ThrowRay(eventData, _unitsManager.CreateUnits, _basesController.UnSelectedBases);
        }

        private void ThrowRay(PointerEventData eventData, Action<RaycastHit2D, Squad> method, Action missed = null)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100.0f);
            if (hit != null && hit.collider != null)
            {
                method?.Invoke(hit, _currentSquad);
            }
            else
            {
                missed?.Invoke();
            }
        }

        private void Update()
        {
            _basesController.Update();
        }

        public override void Register(IContainerBuilder builder)
        {
            builder.Register<GamefieldController>(Lifetime.Scoped);
        }
    }
}
