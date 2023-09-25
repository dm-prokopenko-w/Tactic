using System;
using Core;
using UnityEngine;
using BaseSystem;
using UnityEngine.EventSystems;
using VContainer;
using AISystem;

namespace GameplaySystem
{
    public class GameplayController : MonoBehaviour
    {
        [Inject] private ControlModule _control;

        [Inject] private GameplayManager _gameplay;
        [Inject] private BasesController _basesController;
        [Inject] private UnitsManager _unitsManager;
        [Inject] private AIController _ai;

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private GameData _gameData;

        private Squad _currentSquad = Squad.None;

        private void Start()
        {
            InitSystem();
            Subscribe();
        }

        private void InitSystem()
        {
            var bases = FindObjectsOfType<Base>();
            _basesController.Init(bases, ChangeSquad);
            _unitsManager.Init(_unitPrefab, _parent);
            _ai.Init(_gameData.Enemy);
        }

        private void Subscribe()
        {
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
            ThrowRay(eventData, _gameplay.CreateUnits, _basesController.UnSelectedBases);
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
    }
}
