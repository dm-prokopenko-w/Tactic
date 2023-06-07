using System;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class GameplayController : MonoInstaller
{
    [Inject] private ControlModule _control;
    [Inject] private ObjectPoolModule _poolModule;

    [SerializeField] private GameObject _unitPrefab;

    public Action OnUpdateBases;
    
    private Vector2 _lastPos;
    private BasesController _basesController;

    public override void InstallBindings()
    {
        Container.Bind<GameplayController>().FromInstance(this).AsSingle().NonLazy();
    }

    private void Awake()
    {
        _basesController = new BasesController(_unitPrefab, _poolModule);
        
        _control.TouchStart += TouchStart;
        _control.TouchEnd += TouchEnd;
        _control.TouchMoved += TouchMoved;
    }

    private void OnDestroy()
    {
        _control.TouchStart -= TouchStart;
        _control.TouchEnd -= TouchEnd;
        _control.TouchMoved -= TouchMoved;
    }

    public void AddBase(Base b) => _basesController.AddedNewBase(b);

    private void TouchStart(PointerEventData eventData)
    {
    }

    private void TouchMoved(PointerEventData eventData)
    {
        ThrowRay(eventData, _basesController.AddedBases);
    }

    private void TouchEnd(PointerEventData eventData)
    {
        ThrowRay(eventData, _basesController.CreateUnits);
    }

    private void ThrowRay(PointerEventData eventData, Action<RaycastHit2D> method)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100.0f);
        if (hit != null && hit.collider != null)
        {
            method?.Invoke(hit);
        }
    }

    private void Update()
    {
        _basesController.UpdateUnits(OnUpdateBases);
    }
}