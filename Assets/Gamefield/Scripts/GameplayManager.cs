using BaseSystem;
using UnitSystem;

namespace GameplaySystem
{
    public class GameplayManager
    {
        public void OnTriggerWithBase(Unit unit, Base targetItem)
        {
            if (unit.GetSquad() == targetItem.GetSquad())
            {
                if (unit.GetTargetBase().GetCollider() == targetItem.GetCollider())
                {
                    targetItem.AddedUnit(1);
                }
            }
            else
            {
                if (targetItem.GetCountUnits() > 0)
                {
                    targetItem.AddedUnit(-1);
                }
                else
                {
                    targetItem.SetSquad(unit.GetSquad());
                    targetItem.ChangeColor(unit.GetColor());
                    targetItem.AddedUnit(1);
                }
            }
        }
    }
}