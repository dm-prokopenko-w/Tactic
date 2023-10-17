using BaseSystem;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameplaySystem
{
    public class GameplayManager
    {
        [Inject] private UnitsManager _unitsManager;
        [Inject] private BasesController _basesController;
        [Inject] private CameraController _cameraController;

        public void CreateUnits(RaycastHit2D hit, Squad squad)
        {
            (BaseItem targetBase, List<BaseItem> selectedBases) = _basesController.GetBasesForCreateUnits(hit.collider);
            _unitsManager.CreateUnits(selectedBases, targetBase);
        }
    }
}