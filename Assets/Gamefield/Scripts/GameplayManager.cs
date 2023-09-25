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

        public void CreateUnits(RaycastHit2D hit, Squad squad)
        {
            (Base targetBase, List<Base> selectedBase) = _basesController.GetBasesForCreateUnits(hit.collider);

            foreach (Base b in selectedBase)
            {
                _unitsManager.CreateUnit(b, targetBase);
            }
        }
    }
}