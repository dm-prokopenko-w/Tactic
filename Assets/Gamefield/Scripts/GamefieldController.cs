using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class GamefieldController : MonoInstaller
{
    [Inject] private ControlModule _control;
    [Inject] private ObjectPoolModule _poolModule;

    [SerializeField] private List<GameplayData> _data;
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Transform _parent;

    private GameplayManager _gameplay;
    
    public Action OnUpdateBases;

    private Vector2 _lastPos;

    public override void InstallBindings() =>
        Container.Bind<GamefieldController>().FromInstance(this).AsSingle().NonLazy();

    private void Awake()
    {
        _gameplay = new GameplayManager(_data, _unitPrefab, _poolModule, _parent);
        _control.TouchStart += TouchStart;
        _control.TouchEnd += TouchEnd;
        _control.TouchMoved += TouchMoved;
    }

    private void OnDestroy()
    {
        _gameplay.Unsubscribe();
        _control.TouchStart -= TouchStart;
        _control.TouchEnd -= TouchEnd;
        _control.TouchMoved -= TouchMoved;
    }

    public void AddBase(Base b) => _gameplay.AddedNewBase(b);

    private void TouchStart(PointerEventData eventData)
    {
    }

    private void TouchMoved(PointerEventData eventData)
    {
        ThrowRay(eventData, _gameplay.SelectedBase);
    }

    private void TouchEnd(PointerEventData eventData)
    {
        ThrowRay(eventData, _gameplay.CreateUnits);
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
        _gameplay.UpdateUnits(OnUpdateBases);
    }
}

[Serializable]
public class GameplayData
{
    public Squad SquadData;
    public Color ColorData;
}
