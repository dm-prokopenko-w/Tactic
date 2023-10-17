using System;
using UnityEngine;
using BaseSystem;
using UnityEngine.EventSystems;
using VContainer;
using AISystem;
using Core.ControlSystem;

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
        private CameraController _cameraController;
        private bool _isHitBase = false;

        private void Start()
        {
            _cameraController = new CameraController();
            InitSystem();
            Subscribe();
        }

        private async void InitSystem()
        {
            await _basesController.Init(ChangeSquad);
            _unitsManager.Init(_unitPrefab, _parent);
            _ai.Init(_gameData.Enemys);
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
            _isHitBase = true;
            ThrowRay(eventData, TouchStart, () => _isHitBase = false);
        }

        private void TouchStart(RaycastHit2D hit, Squad squad)
        {
            _basesController.SelectedBase(hit, squad);
            _basesController.StartClickOnBase(hit, squad);
        }

        private void TouchMoved(PointerEventData eventData)
        {
            if (_isHitBase)
            {
                ThrowRay(eventData, _basesController.SelectedBase);
            }
            else
            {
                _cameraController.Move(eventData);
            }
        }

        private void TouchEnd(PointerEventData eventData)
        {
            _isHitBase = false;
            ThrowRay(eventData, TouchEnd, _basesController.UnSelectedBases);
        }

        private void TouchEnd(RaycastHit2D hit, Squad squad)
        {
            _gameplay.CreateUnits(hit, squad);
            _basesController.EndClickOnBase(hit, squad);
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